using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class DailyReport : Form
    {
        private Inventory_DflowEntities db;

        public DailyReport()
        {
            InitializeComponent();
        }

        private void DtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Load_Report();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Report()
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    DateTime? _reportDate = (DateTime?)dtpReportDate.Value;

                    var header = db.sp_Report_Header().ToList();

                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsReportHeader", header));

                    var data = db.sp_Daily_Report(_reportDate, SessionHelper.FinYear).ToList();

                    reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "DailyReport.rdlc");
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsDailyReport", data));

                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DailyReport_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Report();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
