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
            frm.Show();
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
    }
}
