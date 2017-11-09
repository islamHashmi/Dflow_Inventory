using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class ProductionReport : Form
    {
        private Inventory_DflowEntities db;

        public ProductionReport()
        {
            InitializeComponent();
        }

        private void ProductionReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.Refresh();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    DateTime? _startDate = dtpStartDate.Checked ? (DateTime?)dtpStartDate.Value : null;
                    DateTime? _endDate = dtpEndDate.Checked ? (DateTime?)dtpEndDate.Value : null;

                    var header = db.sp_Report_Header().ToList();

                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsReportHeader", header));

                    if (chkDetail.Checked)
                    {
                        var detail = db.sp_production_report_detail(SessionHelper.FinYear, _startDate, _endDate).ToList();

                        reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "ProductionReport_Detail.rdlc");
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsProduction_Detail", detail));
                    }
                    else
                    {
                        var summary = db.sp_production_report_summary(SessionHelper.FinYear, _startDate, _endDate).ToList();

                        reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "ProductionReport_Summary.rdlc");
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsProduction_Summary", summary));
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
    }
}
