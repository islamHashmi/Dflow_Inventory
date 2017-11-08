using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Transactions;

namespace Dflow_Inventory.ContentPage
{
    public partial class VoucherPayment : Form
    {
        private Inventory_DflowEntities db;

        private int _voucherId = 0;

        public int VoucherId { get => _voucherId; set => _voucherId = value; }

        public VoucherPayment()
        {
            InitializeComponent();

            Autogenerate_VoucherNumber();

            Clear_Date();

            grpPayment.Visible = false;

            ComboBox();
            Get_Data();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var expenses = db.ExpenseMasters
                                .Select(m => new
                                {
                                    id = m.expenseHeadId,
                                    name = m.expenseName,
                                    active = m.active
                                }).Where(m => m.active == true)
                                .ToList();

                expenses.Insert(0, new { id = 0, name = "--- Select Expense Head ---", active = true });

                cmbExpenseHead.DataSource = expenses;
                cmbExpenseHead.DisplayMember = "name";
                cmbExpenseHead.ValueMember = "id";

                var employees = db.Employees
                                .Select(m => new
                                {
                                    id = m.employeeId,
                                    name = (m.firstName ?? "") + " " + (m.middleName ?? "") + " " + (m.lastName ?? ""),
                                    active = m.active
                                }).Where(m => m.active == true)
                                .ToList();

                employees.Insert(0, new { id = 0, name = "--- Select Employee Name ---", active = true });

                cmbEmployee.DataSource = employees;
                cmbEmployee.DisplayMember = "name";
                cmbEmployee.ValueMember = "id";

                var paymentModes = db.PaymentModes.Select(m => new { paymentCode = m.paymentCode, paymentDescription = m.paymentDescription }).ToList();

                CmbPaymentMode.DataSource = paymentModes;
                CmbPaymentMode.DisplayMember = "paymentDescription";
                CmbPaymentMode.ValueMember = "paymentCode";
            }
        }

        private void Autogenerate_VoucherNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(db.voucherHeaders.Max(m => m.voucherNumber), out int _voucherNo);

                TxtVoucherNo.Text = $"{_voucherNo + 1}";
            }
        }

        private void Clear_Date()
        {
            DtpChqDate.CustomFormat = " ";
            DtpChqDate.Format = DateTimePickerFormat.Custom;
        }

        private void Clear_Fields()
        {
            Autogenerate_VoucherNumber();

            Clear_Date();

            VoucherId = 0;

            txtName.Text = string.Empty;
            TxtAmount.Text = string.Empty;
            CmbPaymentMode.SelectedIndex = 0;
            TxtNarration.Text = string.Empty;
            TxtBankName.Text = string.Empty;
            TxtChqueNo.Text = string.Empty;
            lblCustomerId.Text = string.Empty;
            cmbExpenseHead.SelectedIndex = 0;
            cmbEmployee.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Fields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) && cmbEmployee.SelectedIndex == 0)
                {
                    MessageBox.Show(text: "Please select employee or enter name.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Exclamation);
                    cmbEmployee.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtAmount.Text))
                {
                    MessageBox.Show(text: "Amount is mandatory.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Exclamation);
                    TxtAmount.Focus();
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            voucherHeader vh = new voucherHeader();

                            if (VoucherId == 0)
                            {
                                db.voucherHeaders.Add(vh);

                                vh.voucherNumber = TxtVoucherNo.Text.Trim();
                                vh.entryBy = SessionHelper.UserId;
                                vh.entryDate = DateTime.Now;
                            }
                            else
                            {
                                vh = db.voucherHeaders.FirstOrDefault(m => m.voucherId == VoucherId);

                                vh.updatedBy = SessionHelper.UserId;
                                vh.updatedDate = DateTime.Now;
                            }

                            decimal.TryParse(TxtAmount.Text, out decimal _amount);

                            vh.voucherType = "P";
                            vh.name = cmbEmployee.SelectedIndex == 0 ? txtName.Text : cmbEmployee.SelectedText;
                            vh.employeeId = Convert.ToInt32(cmbEmployee.SelectedValue) == 0 ? null : (int?)Convert.ToInt32(cmbEmployee.SelectedValue);
                            vh.expenseId = Convert.ToInt32(cmbExpenseHead.SelectedValue) == 0 ? null : (int?)Convert.ToInt32(cmbExpenseHead.SelectedValue);
                            vh.voucherDate = (DateTime)CommanMethods.ConvertDate(DtpVoucherDate.Text);
                            vh.amount = _amount;
                            vh.paymentMode = Convert.ToString(CmbPaymentMode.SelectedValue);
                            vh.bankName = string.IsNullOrEmpty(TxtBankName.Text) ? null : TxtBankName.Text;
                            vh.chequeNo = string.IsNullOrEmpty(TxtChqueNo.Text) ? null : TxtChqueNo.Text;
                            vh.chequeDate = CommanMethods.ConvertDate(DtpChqDate.Text);
                            vh.narration = string.IsNullOrEmpty(TxtNarration.Text) ? null : TxtNarration.Text;
                            vh.finYear = SessionHelper.FinYear;

                            db.SaveChanges();

                            Clear_Fields();

                            Get_Data();

                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from a in db.voucherHeaders
                                      join b in db.ExpenseMasters on a.expenseId equals b.expenseHeadId into exp
                                      from b in exp.DefaultIfEmpty()
                                      join e in db.Employees on a.employeeId equals e.employeeId into emp
                                      from e in emp.DefaultIfEmpty()
                                      join c in db.PaymentModes on a.paymentMode equals c.paymentCode into pay
                                      from c in pay.DefaultIfEmpty()
                                      where a.voucherType == "P"
                                      select new
                                      {
                                          voucherId = a.voucherId,
                                          voucherNumber = a.voucherNumber,
                                          voucherDate = a.voucherDate,
                                          expenseName = b.expenseName,
                                          employeeName = e.firstName + " " + e.middleName + " " + e.lastName,
                                          name = a.name,
                                          amount = a.amount,
                                          paymentMode = c.paymentDescription,
                                          bankName = a.bankName,
                                          chqNumber = a.chequeNo,
                                          chqDate = a.chequeDate
                                      }).ToList();

                Set_Column();
            }
        }

        private void Set_Column()
        {
            DgvList.Columns["voucherId"].Visible = false;

            DgvList.Columns["voucherNumber"].HeaderText = "Voucher #";
            DgvList.Columns["voucherNumber"].DisplayIndex = 0;

            DgvList.Columns["voucherDate"].HeaderText = "Voucher Date";
            DgvList.Columns["voucherDate"].DisplayIndex = 1;

            DgvList.Columns["expenseName"].HeaderText = "Expense Name";
            DgvList.Columns["expenseName"].DisplayIndex = 2;

            DgvList.Columns["employeeName"].HeaderText = "Employee Name";
            DgvList.Columns["employeeName"].DisplayIndex = 3;

            DgvList.Columns["name"].HeaderText = "Name";
            DgvList.Columns["name"].DisplayIndex = 4;

            DgvList.Columns["amount"].HeaderText = "Amount";
            DgvList.Columns["amount"].DisplayIndex = 5;

            DgvList.Columns["paymentMode"].HeaderText = "payment Mode";
            DgvList.Columns["paymentMode"].DisplayIndex = 6;

            DgvList.Columns["bankName"].HeaderText = "Bank Name";
            DgvList.Columns["bankName"].DisplayIndex = 7;

            DgvList.Columns["chqNumber"].HeaderText = "Cheque No.";
            DgvList.Columns["chqNumber"].DisplayIndex = 8;

            DgvList.Columns["chqDate"].HeaderText = "Cheque Date";
            DgvList.Columns["chqDate"].DisplayIndex = 9;
        }

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                int.TryParse(Convert.ToString(DgvList["voucherId", RowIndex].Value), out int _id);

                using (db = new Inventory_DflowEntities())
                {
                    var voucher = (from a in db.voucherHeaders
                                   join b in db.CustomerMasters on a.customerId equals b.customerId into cus
                                   from b in cus.DefaultIfEmpty()
                                   where a.voucherId == _id
                                   select new
                                   {
                                       voucherId = a.voucherId,
                                       voucherNo = a.voucherNumber,
                                       voucherDate = a.voucherDate,
                                       customer = b.customerName,
                                       amount = a.amount,
                                       paymentMode = a.paymentMode,
                                       bankName = a.bankName,
                                       chqNumber = a.chequeNo,
                                       chqDate = a.chequeDate,
                                       narration = a.narration
                                   }).FirstOrDefault();

                    if (voucher != null)
                    {
                        VoucherId = voucher.voucherId;
                        TxtVoucherNo.Text = voucher.voucherNo;
                        DtpVoucherDate.Text = voucher.voucherDate.ToString("dd/MM/yyyy");
                        txtName.Text = voucher.customer;
                        TxtAmount.Text = voucher.amount.ToString();
                        CmbPaymentMode.SelectedValue = voucher.paymentMode;
                        TxtBankName.Text = voucher.bankName;
                        TxtChqueNo.Text = voucher.chqNumber;
                        DtpChqDate.Text = voucher.chqDate == null ? string.Empty : Convert.ToDateTime(voucher.chqDate).ToString("dd/MM/yyyy");
                        TxtNarration.Text = voucher.narration;

                        CmbPaymentMode_SelectionChangeCommitted(null, null);
                    }

                    tabControl1.SelectedIndex = 0;
                }
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
                MessageBox.Show(ex.Message);
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
                        if (DialogResult.No == MessageBox.Show("Are sure to delete this record ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            return;

                        int.TryParse(s: Convert.ToString(DgvList["voucherId", DgvList.CurrentCell.RowIndex].Value), result: out int _id);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.voucherHeaders.FirstOrDefault(m => m.voucherId == _id) != null)
                                db.voucherHeaders.Remove(db.voucherHeaders.FirstOrDefault(m => m.voucherId == _id));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(CmbPaymentMode.SelectedValue) == "CQ")
                {
                    grpPayment.Visible = true;
                }
                else
                {
                    grpPayment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DtpChqDate_ValueChanged(object sender, EventArgs e)
        {
            DtpChqDate.CustomFormat = "dd/MM/yyyy";
        }
    }
}
