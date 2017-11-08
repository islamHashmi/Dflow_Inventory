using Dflow_Inventory.DataContext;
using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace Dflow_Inventory.ContentPage
{
    public partial class InvoicePrint : Form
    {
        private Inventory_DflowEntities db;

        private int _invoiceId = 0;

        public InvoicePrint(int invoiceId)
        {
            InitializeComponent();

            _invoiceId = invoiceId;
        }

        private void InvoicePrint_Load(object sender, EventArgs e)
        {
            Load_Report();
            this.reportViewer1.RefreshReport();
        }

        private void Load_Report()
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    var data = db.sp_invoice_print(_invoiceId).ToList();

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "Invoice.rdlc");
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsInvoice", data));

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
