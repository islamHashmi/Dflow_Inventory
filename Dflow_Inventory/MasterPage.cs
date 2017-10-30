using Dflow_Inventory.ContentPage;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Dflow_Inventory
{
    public partial class MasterPage : Form
    {
        public MasterPage()
        {
            InitializeComponent();

            Get_StatusStrip();
        }

        private void Get_StatusStrip()
        {
            using (Inventory_DflowEntities db = new Inventory_DflowEntities())
            {
                var fy = db.FinancialYears.FirstOrDefault(m => m.finYear == SessionHelper.FinYear);

                if (fy != null)
                {
                    toolStripFinYear.Text = "Financial year : " + fy.finYearDescription;
                }
            }

            toolStripLoginUser.Text = "Logged In : " + SessionHelper.UserName;
        }

        private void Show_Form(Form frm)
        {
            bool _isFormOpen = false;

            FormCollection fc = Application.OpenForms;

            foreach (Form _frm in fc)
            {
                if(_frm.Text == frm.Text)
                {
                    _isFormOpen = true;
                    _frm.BringToFront();
                }
            }

            if(!_isFormOpen)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }            
        }
        
        private void UnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new UnitMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new ItemMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new CustomerMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void VendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new VendorMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DesignationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new DesignationMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new EmployeeMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new ExpenseMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void PurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new PurchaseOrder());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void SalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new SalesInvoice());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new VoucherReceipt());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void PaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new VoucherPayment());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ApplicationParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new ApplicationParameter());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void UserMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new UserMaster());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void MasterPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CloseAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void StockRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new StockReport());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SessionHelper.FinYear = string.Empty;
                SessionHelper.HsnCode = string.Empty;
                SessionHelper.ShortFinYear = string.Empty;
                SessionHelper.UserGroupId = 0;
                SessionHelper.UserId = 0;
                SessionHelper.UserName = string.Empty;

                this.Hide();

                Login frm = new Login();
                frm.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new SalesReport());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new PurchaseReport());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dailyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new DailyReport());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void voucherReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new VoucherReport());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
