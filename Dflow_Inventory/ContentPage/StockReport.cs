using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class StockReport : Form
    {
        private Inventory_DflowEntities db;

        public StockReport()
        {
            InitializeComponent();

            ComboBox();
        }

        private void StockReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var items = db.ItemMasters
                            .Select(m => new
                            {
                                itemId = m.itemId,
                                itemName = m.itemName,
                                active = m.active
                            }).Where(m => m.active == true)
                            .ToList();

                items.Insert(0, new { itemId = 0, itemName = "--- Select Item Name ---", active = true });

                cmbItemName.DataSource = items;
                cmbItemName.DisplayMember = "itemName";
                cmbItemName.ValueMember = "itemId";
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    DateTime? _startDate = dtpStartDate.Checked ? (DateTime?)dtpStartDate.Value : null;
                    DateTime? _endDate = dtpEndDate.Checked ? (DateTime?)dtpEndDate.Value : null;
                    int? _itemId = cmbItemName.SelectedIndex == 0 ? null : (int?)Convert.ToInt32(cmbItemName.SelectedValue);

                    var header = db.sp_Report_Header().ToList();

                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsReportHeader", header));

                    if (chkDetail.Checked)
                    {
                        var data = db.sp_Stock_Report_Detail(SessionHelper.FinYear, _itemId, _startDate, _endDate).ToList();

                        reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "StockReport_Detail.rdlc");
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));
                    }
                    else
                    {
                        var summary = db.sp_Stock_Report_Summary().ToList();

                        reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "StockReport_Summary.rdlc");
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsStock_Summary", summary));
                    }

                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChkDetail_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtpStartDate.Enabled = chkDetail.Checked;
                dtpEndDate.Enabled = chkDetail.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
