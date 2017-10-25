using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dflow_Inventory.DataSets;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Diagnostics;

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
                var items = db.Item_Master
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

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    DateTime? _startDate = dtpStartDate.Checked ? (DateTime?)dtpStartDate.Value : null;
                    DateTime? _endDate = dtpEndDate.Checked ? (DateTime?)dtpEndDate.Value : null;
                    int? _itemId = cmbItemName.SelectedIndex == 0 ? null : (int?)Convert.ToInt32(cmbItemName.SelectedValue);

                    var data = db.sp_Stock_Report(_itemId, _startDate, _endDate).ToList();

                    string path = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "StockReport.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportPath = path;
                    ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
                    reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
