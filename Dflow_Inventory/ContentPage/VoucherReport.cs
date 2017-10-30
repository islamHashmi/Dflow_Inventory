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
    public partial class VoucherReport : Form
    {
        private Inventory_DflowEntities db;

        public VoucherReport()
        {
            InitializeComponent();

            ComboBox();
        }

        private void VoucherReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var voucherType = new Dictionary<string, string>
                {
                    { "0", "--- Select Voucher Type ---" },
                    { "R", "Receipt" },
                    { "P", "Payment" }
                };

                cmbVoucherType.DataSource = voucherType.ToList();
                cmbVoucherType.DisplayMember = "Value";
                cmbVoucherType.ValueMember = "Key";
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    string voucherType = cmbVoucherType.SelectedIndex == 0 ? null : Convert.ToString(cmbVoucherType.SelectedValue);
                    DateTime? _startDate = dtpStartDate.Checked ? (DateTime?)dtpStartDate.Value : null;
                    DateTime? _endDate = dtpEndDate.Checked ? (DateTime?)dtpEndDate.Value : null;
                    int? _Id = cmbName.SelectedIndex == 0 ? null : (int?)Convert.ToInt32(cmbName.SelectedValue);
                    int? employeeId = 0, customerId = 0;

                    employeeId = voucherType == "P" ? _Id : null;
                    customerId = voucherType == "R" ? _Id : null;

                    var header = db.sp_Report_Header().ToList();

                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsReportHeader", header));

                    var data = db.sp_voucher_report(SessionHelper.FinYear, voucherType, employeeId, customerId, _startDate, _endDate).ToList();

                    reportViewer1.LocalReport.ReportPath = Path.Combine(ConfigurationManager.AppSettings["reportPath"], "VoucherReport.rdlc");
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsVoucher", data));

                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CmbVoucherType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    if (cmbVoucherType.SelectedIndex == 0)
                    {
                        cmbName.DataSource = null;
                        return;
                    }

                    if (Convert.ToString(cmbVoucherType.SelectedValue) == "R")
                    {
                        var employees = db.Employees.Select(m => new
                        {
                            employeeId = m.employeeId,
                            employeeName = m.firstName + " " + m.lastName,
                            active = m.active
                        }).Where(m => m.active == true).ToList();

                        employees.Insert(0, new { employeeId = 0, employeeName = "--- Select Name ---", active = true });

                        cmbName.DataSource = employees;
                        cmbName.DisplayMember = "employeeName";
                        cmbName.ValueMember = "employeeId";
                    }
                    else
                    {
                        var customers = db.Customer_Master.Select(m => new
                        {
                            customerId = m.customerId,
                            customerName = m.customerName,
                            active = m.active
                        }).Where(m => m.active == true).ToList();

                        customers.Insert(0, new { customerId = 0, customerName = "--- Select Name ---", active = true });

                        cmbName.DataSource = customers;
                        cmbName.DisplayMember = "customerName";
                        cmbName.ValueMember = "customerId";
                    }
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
