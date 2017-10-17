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
using System.Transactions;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class VoucherReceipt : Form
    {
        private Inventory_DflowEntities db;

        private int _voucherId = 0;

        public VoucherReceipt()
        {
            InitializeComponent();

            Autogenerate_VoucherNumber();

            Clear_Date();

            lblNaration.Location = new Point(52, 174);
            TxtNarration.Location = new Point(174, 172);

            grpPayment.Visible = false;

            ComboBox();
            Get_Data();
        }

        private void Clear_Date()
        {
            DtpChqDate.CustomFormat = " ";
            DtpChqDate.Format = DateTimePickerFormat.Custom;
        }

        private void Autogenerate_VoucherNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                var voucherNumber = db.voucherHeaders.Max(m => m.voucherNumber);

                int _voucherNo = 0;

                int.TryParse(voucherNumber, out _voucherNo);

                TxtVoucherNo.Text = string.Format("{0}", _voucherNo + 1);
            }
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var paymentModes = db.PaymentModes.Select(m => new { paymentCode = m.paymentCode, paymentDescription = m.paymentDescription }).ToList();

                cmbPaymentMode.DataSource = paymentModes;
                cmbPaymentMode.DisplayMember = "paymentDescription";
                cmbPaymentMode.ValueMember = "paymentCode";
            }
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void cmbPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(cmbPaymentMode.SelectedValue) == "CQ")
                {
                    lblNaration.Location = new Point(52, 302);
                    TxtNarration.Location = new Point(174, 299);

                    grpPayment.Visible = true;
                }
                else
                {
                    lblNaration.Location = new Point(52, 174);
                    TxtNarration.Location = new Point(174, 172);

                    grpPayment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtInvoiceNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    var invoices = db.InvoiceHeaders.FirstOrDefault(m => m.invoiceNumber == TxtInvoiceNo.Text.Trim());

                    if (invoices != null)
                    {
                        TxtCustomerName.Text = invoices.CustomerMaster.customerName;
                        TxtAmount.Text = invoices.totalAmount.ToString();
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
                using (db = new Inventory_DflowEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            voucherHeader vh = new voucherHeader();

                            if (_voucherId == 0)
                            {
                                db.voucherHeaders.Add(vh);

                                vh.voucherNumber = TxtVoucherNo.Text.Trim();
                                vh.entryBy = SessionHelper.UserId;
                                vh.entryDate = DateTime.Now;
                            }
                            else
                            {
                                vh = db.voucherHeaders.FirstOrDefault(m => m.voucherId == _voucherId);

                                vh.updatedBy = SessionHelper.UserId;
                                vh.updatedDate = DateTime.Now;
                            }

                            decimal _amount = 0;

                            decimal.TryParse(TxtAmount.Text, out _amount);

                            vh.voucherDate = (DateTime)CommanMethods.ConvertDate(DtpVoucherDate.Text);
                            vh.invoiceNo = string.IsNullOrEmpty(TxtInvoiceNo.Text) ? null : TxtInvoiceNo.Text.Trim();
                            vh.amount = _amount == 0 ? null : (decimal?)_amount;
                            vh.paymentMode = Convert.ToString(cmbPaymentMode.SelectedValue);
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
                        catch (Exception)
                        {
                            scope.Dispose();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DtpChqDate_ValueChanged(object sender, EventArgs e)
        {
            DtpChqDate.CustomFormat = "dd/MM/yyyy";
        }

        private void Clear_Fields()
        {
            Autogenerate_VoucherNumber();
            Clear_Date();

            _voucherId = 0;

            TxtInvoiceNo.Text = string.Empty;
            TxtCustomerName.Text = string.Empty;
            TxtAmount.Text = string.Empty;
            cmbPaymentMode.SelectedIndex = 0;
            TxtNarration.Text = string.Empty;
            TxtBankName.Text = string.Empty;
            TxtChqueNo.Text = string.Empty;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                var vouchers = (from a in db.voucherHeaders
                                join b in db.InvoiceHeaders on a.invoiceNo equals b.invoiceNumber into inv
                                from b in inv.DefaultIfEmpty()
                                join c in db.PaymentModes on a.paymentMode equals c.paymentCode into pay
                                from c in pay.DefaultIfEmpty()
                                select new
                                {
                                    voucherId = a.voucherId,
                                    voucherNumber = a.voucherNumber,
                                    voucherDate = a.voucherDate,
                                    invoiceNumber = a.invoiceNo,
                                    customer = b.CustomerMaster.customerName,
                                    amount = a.amount,
                                    paymentMode = c.paymentDescription,
                                    bankName = a.bankName,
                                    chqNumber = a.chequeNo,
                                    chqDate = a.chequeDate
                                }).ToList();

                DgvList.DataSource = vouchers;

                Set_Column();
            }
        }

        private void Set_Column()
        {
            DgvList.Columns["voucherId"].Visible = false;

            DgvList.Columns["voucherNumber"].HeaderText = "Voucher #";
            DgvList.Columns["voucherNumber"].DisplayIndex = 0;

            DgvList.Columns["voucherDate"].HeaderText = "VOucher Date";
            DgvList.Columns["voucherDate"].DisplayIndex = 0;

            DgvList.Columns["invoiceNumber"].HeaderText = "Invoice #";
            DgvList.Columns["invoiceNumber"].DisplayIndex = 0;

            DgvList.Columns["customer"].HeaderText = "Customer Name";
            DgvList.Columns["customer"].DisplayIndex = 0;

            DgvList.Columns["amount"].HeaderText = "Amount";
            DgvList.Columns["amount"].DisplayIndex = 0;

            DgvList.Columns["paymentMode"].HeaderText = "payment Mode";
            DgvList.Columns["paymentMode"].DisplayIndex = 0;

            DgvList.Columns["bankName"].HeaderText = "Bank Name";
            DgvList.Columns["bankName"].DisplayIndex = 0;

            DgvList.Columns["chqNumber"].HeaderText = "Cheque No.";
            DgvList.Columns["chqNumber"].DisplayIndex = 0;

            DgvList.Columns["chqDate"].HeaderText = "Cheque Date";
            DgvList.Columns["chqDate"].DisplayIndex = 0;
        }

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                int _id = 0;

                int.TryParse(Convert.ToString(DgvList["voucherId", RowIndex].Value), out _id);

                using (db = new Inventory_DflowEntities())
                {
                    var voucher = (from a in db.voucherHeaders
                                   join b in db.InvoiceHeaders on a.invoiceNo equals b.invoiceNumber into inv
                                   from b in inv.DefaultIfEmpty()
                                   join c in db.Customer_Master on b.customerId equals c.customerId into cust
                                   from c in cust.DefaultIfEmpty()
                                   where a.voucherId == _id
                                   select new
                                   {
                                       voucherId = a.voucherId,
                                       voucherNo = a.voucherNumber,
                                       voucherDate = a.voucherDate,
                                       invoiceNo = a.invoiceNo,
                                       customer = c.customerName,
                                       amount = a.amount,
                                       paymentMode = a.paymentMode,
                                       bankName = a.bankName,
                                       chqNumber = a.chequeNo,
                                       chqDate = a.chequeDate,
                                       narration = a.narration
                                   }).FirstOrDefault();

                    if (voucher != null)
                    {
                        _voucherId = voucher.voucherId;
                        TxtVoucherNo.Text = voucher.voucherNo;
                        DtpVoucherDate.Text = voucher.voucherDate.ToString("dd/MM/yyyy");
                        TxtInvoiceNo.Text = voucher.invoiceNo;
                        TxtCustomerName.Text = voucher.customer;
                        TxtAmount.Text = voucher.amount.ToString();
                        cmbPaymentMode.SelectedValue = voucher.paymentMode;
                        TxtBankName.Text = voucher.bankName;
                        TxtChqueNo.Text = voucher.chqNumber;
                        DtpChqDate.Text = voucher.chqDate == null ? string.Empty : Convert.ToDateTime(voucher.chqDate).ToString("dd/MM/yyyy");
                        TxtNarration.Text = voucher.narration;

                        cmbPaymentMode_SelectionChangeCommitted(null, null);
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
                        if (DialogResult.No == MessageBox.Show("Are sure to delete this record ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            return;

                        int _id = 0;

                        int.TryParse(Convert.ToString(DgvList["voucherId", DgvList.CurrentCell.RowIndex].Value), out _id);

                        using (db = new Inventory_DflowEntities())
                        {
                            var voucher = db.voucherHeaders.FirstOrDefault(m => m.voucherId == _id);

                            if (voucher != null)
                                db.voucherHeaders.Remove(voucher);
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
