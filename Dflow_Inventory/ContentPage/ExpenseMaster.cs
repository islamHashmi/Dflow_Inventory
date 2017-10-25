using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class ExpenseMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _expenseHeadId = 0;

        public int ExpenseHeadId { get => _expenseHeadId; set => _expenseHeadId = value; }

        public ExpenseMaster()
        {
            InitializeComponent();

            Get_Data();
        }

        private void TxtExpenseName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtExpenseName.Text))
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        if (db.Expense_Master.FirstOrDefault(m => m.expenseName == TxtExpenseName.Text.Trim()) != null)
                        {
                            MessageBox.Show("Expense Name already exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtExpenseName.Text))
                {
                    MessageBox.Show("Expense Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TxtExpenseName.Focus();
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Expense_Master em = new Expense_Master();

                    if (ExpenseHeadId == 0)
                    {
                        db.Expense_Master.Add(em);
                        em.entryBy = SessionHelper.UserId;
                        em.entryDate = DateTime.Now;
                        em.active = true;
                    }
                    else
                    {
                        em = db.Expense_Master.FirstOrDefault(m => m.expenseHeadId == ExpenseHeadId);
                        em.updatedBy = SessionHelper.UserId;
                        em.updatedDate = DateTime.Now;
                    }

                    em.expenseName = TxtExpenseName.Text.Trim();

                    db.SaveChanges();

                    Clear_Controls();

                    Get_Data();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Controls();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Clear_Controls()
        {
            TxtExpenseName.Text = string.Empty;

            ExpenseHeadId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from a in db.Expense_Master
                                      where a.active == true
                                      select new
                                      {
                                          expenseHeadId = a.expenseHeadId,
                                          expenseName = a.expenseName,
                                          active = a.active
                                      }).ToList();

                Set_Column();
            }
        }

        private void Set_Column()
        {
            DgvList.Columns["expenseHeadId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["expenseName"].HeaderText = "Expense Name";
            DgvList.Columns["expenseName"].DisplayIndex = 0;
        }

        private void DgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,

                LineAlignment = StringAlignment.Center
            };

            Size textSize = TextRenderer.MeasureText(rowIdx, this.Font);

            if (grid.RowHeadersWidth < textSize.Width + 40)
            {
                grid.RowHeadersWidth = textSize.Width + 40;
            }

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                Clear_Controls();

                int.TryParse(s: Convert.ToString(DgvList["expenseHeadId", RowIndex].Value), result: out int _id);

                if (_id != 0)
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        var expense = db.Expense_Master.FirstOrDefault(m => m.expenseHeadId == _id);

                        if (expense != null)
                        {
                            ExpenseHeadId = expense.expenseHeadId;
                            TxtExpenseName.Text = expense.expenseName;
                        }
                    }
                }
                tabControl1.SelectedIndex = 0;
            }
        }

        private void DgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CellContentClick(e.ColumnIndex, e.RowIndex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DgvList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DgvList.CurrentCell != null)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        CellContentClick(DgvList.CurrentCell.ColumnIndex, DgvList.CurrentCell.RowIndex);
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        if (DialogResult.No == MessageBox.Show("Are you sure to delete this record ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            return;
                        
                        int.TryParse(Convert.ToString(DgvList["expenseHeadId", DgvList.CurrentCell.RowIndex].Value), out int _id);

                        using (db = new Inventory_DflowEntities())
                        {
                            var expense = db.Expense_Master.FirstOrDefault(m => m.expenseHeadId == _id);

                            if (expense != null)
                            {
                                expense.active = false;
                                expense.updatedBy = SessionHelper.UserId;
                                expense.updatedDate = DateTime.Now;

                                db.SaveChanges();
                            }
                        }
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
