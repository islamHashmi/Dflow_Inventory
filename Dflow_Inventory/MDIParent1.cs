using Dflow_Inventory.ContentPage;
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
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void Show_Form(Form frm)
        {
            frm.Show();
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show_Form(new SupplierMaster());
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

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
