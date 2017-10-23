using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class VoucherPayment : Form
    {
        private Inventory_DflowEntities db;

        public VoucherPayment()
        {
            InitializeComponent();

            ComboBox();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var expenses = db.Expense_Master
                                .Select(m => new
                                {
                                    id = m.expenseHeadId,
                                    name = m.expenseName,
                                    active = m.active
                                }).Where(m => m.active == true)
                                .ToList();

                cmbExpenseHead.DataSource = expenses;
                cmbExpenseHead.DisplayMember = "name";
                cmbExpenseHead.ValueMember = "id";

                var employees = db.Employees
                                .Select(m => new
                                {
                                    id = m.employeeId,
                                    name = m.firstName + " " + m.middleName + " " + m.lastName,
                                    active = m.active
                                }).Where(m => m.active == true)
                                .ToList();

                cmbEmployee.DataSource = employees;
                cmbEmployee.DisplayMember = "name";
                cmbEmployee.ValueMember = "id";
            }
        }

    }
}
