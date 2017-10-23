using Dflow_Inventory.ContentPage;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void unitsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void vendorsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void designationsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void applicationParameterToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void userMasterToolStripMenuItem1_Click(object sender, EventArgs e)
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
    }
}
