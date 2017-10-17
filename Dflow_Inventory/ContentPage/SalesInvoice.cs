using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class SalesInvoice : Form
    {
        private Inventory_DflowEntities db;

        private int _invoiceId = 0;

        public SalesInvoice()
        {
            InitializeComponent();

            Autogenerate_InvoiceNumber();
            Get_Data();
        }

        private void DgvItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void DgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DgvItems.CurrentCell.ColumnIndex == 0)
            {
                AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();

                using (db = new Inventory_DflowEntities())
                {
                    var items = db.Item_Master.Where(x => x.active == true);

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            string name = item.itemName;

                            stringCollection.Add(name);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data not Found");
                    }
                }

                TextBox txtItemName = e.Control as TextBox;

                if (txtItemName != null)
                {
                    txtItemName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtItemName.AutoCompleteCustomSource = stringCollection;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }

            e.Control.KeyPress -= new KeyPressEventHandler(Column_Integer_KeyPress);
            e.Control.KeyPress -= new KeyPressEventHandler(Column_Decimal_KeyPress);

            if (DgvItems.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Decimal_KeyPress);
                }
            }

            if (DgvItems.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Integer_KeyPress);
                }
            }

            if (DgvItems.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Decimal_KeyPress);
                }
            }
        }

        private void Column_Integer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void Column_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    string itemName = Convert.ToString(DgvItems[e.ColumnIndex, e.RowIndex].Value);

                    string[] split = itemName.Split('-');

                    string itemCode = itemName;

                    using (db = new Inventory_DflowEntities())
                    {
                        var item = (from m in db.Item_Master
                                    join b in db.Unit_Master on m.unitId equals b.unitId into unit
                                    from b in unit.DefaultIfEmpty()
                                    where m.itemName == itemCode
                                    select new
                                    {
                                        itemId = m.itemId,
                                        itemCode = m.itemCode,
                                        itemName = m.itemName,
                                        description = m.itemDescription,
                                        unitId = m.unitId == null ? 0 : m.unitId,
                                        unitCode = b.unitCode,
                                        sellingPrice = m.sellingPrice,
                                        openingStock = m.openingStock,
                                        active = m.active
                                    }).FirstOrDefault();

                        if (item != null)
                        {
                            DgvItems["itemId", e.RowIndex].Value = item.itemId;
                            DgvItems["Col_Unit", e.RowIndex].Value = item.unitCode;
                        }
                        else
                        {
                            DgvItems["itemId", e.RowIndex].Value = DBNull.Value;
                            DgvItems["Col_Unit", e.RowIndex].Value = DBNull.Value;
                        }
                    }
                }

                Calculate_Amount(e.ColumnIndex, e.RowIndex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Calculate_Amount(int ColumnIndex, int RowIndex)
        {
            decimal _rate = 0, _quantity = 0, _amount = 0;

            decimal.TryParse(Convert.ToString(DgvItems["Col_Rate", RowIndex].Value), out _rate);
            decimal.TryParse(Convert.ToString(DgvItems["Col_Quantity", RowIndex].Value), out _quantity);
            decimal.TryParse(Convert.ToString(DgvItems["Col_Amount", RowIndex].Value), out _amount);

            _amount = _rate * _quantity;

            DgvItems["Col_Rate", RowIndex].Value = string.Format("{0:0.00}", _rate);
            DgvItems["Col_Amount", RowIndex].Value = string.Format("{0:0.00}", _amount);

            Calculate_Total_Amount();
        }

        private void Calculate_Total_Amount()
        {
            decimal _amount = 0, _totalAmount = 0;

            foreach (DataGridViewRow row in DgvItems.Rows)
            {
                decimal.TryParse(Convert.ToString(row.Cells["Col_Amount"].Value), out _amount);

                _totalAmount += _amount;
            }

            TxtTotal.Text = string.Format("{0:0.00}", _totalAmount);

            decimal _discount = 0, _taxableAmt = 0, _cgst1 = 0, _cgst2 = 0, _totalAmt = 0;

            decimal.TryParse(TxtDiscount.Text, out _discount);

            _taxableAmt = _totalAmount - _discount;

            TxtTaxableAmt.Text = string.Format("{0:0.00}", _taxableAmt);

            decimal _gstPercent1 = 0, _gstPercent2 = 0;

            decimal.TryParse(TxtCGST1.Text, out _gstPercent1);
            decimal.TryParse(TxtCGST2.Text, out _gstPercent2);

            _cgst1 = (_taxableAmt * _gstPercent1) / 100;
            _cgst2 = (_taxableAmt * _gstPercent2) / 100;

            TxtCGST1Amt.Text = string.Format("{0:0.00}", _cgst1);

            TxtCGST2Amt.Text = string.Format("{0:0.00}", _cgst2);

            _totalAmt = _taxableAmt + _cgst1 + _cgst2;

            TxtTotalAmt.Text = string.Format("{0:0.00}", _totalAmt);
        }

        private void TxtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    var Vendors = db.Customer_Master
                                    .Select(m => new
                                    {
                                        vendorId = m.customerId,
                                        vendorName = m.customerName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtCustomerName.Text.Trim()))
                                    .ToList();

                    if (Vendors != null)
                    {
                        LstCustomer.DataSource = Vendors;
                        LstCustomer.DisplayMember = "vendorName";
                        LstCustomer.ValueMember = "vendorId";
                        LstCustomer.SelectedIndex = -1;
                    }
                }

                LstCustomer_Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstCustomer_Show()
        {
            LstCustomer.Show();
            LstCustomer.BringToFront();
            LstCustomer.Location = new Point(TxtCustomerName.Location.X, TxtCustomerName.Location.Y + 25);
            LstCustomer.Width = TxtCustomerName.Width;
            LstCustomer.Height = 200;
        }

        private void TxtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    LstCustomer_Show();

                    LstCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string referredById = LstCustomer.SelectedValue.ToString();

                    TxtCustomerName.Text = LstCustomer.Text;

                    lblCustomerId.Text = referredById;

                    LstCustomer.Hide();

                    TxtCustomerName.Focus();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string referredById = LstCustomer.SelectedValue.ToString();

                TxtCustomerName.Text = LstCustomer.Text;

                lblCustomerId.Text = referredById;

                LstCustomer.Hide();

                TxtCustomerName.Focus();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                LstCustomer.Hide();
                TxtCustomerName.Focus();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtCustomerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtCustomerName.Text))
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        int _customerId = 0;

                        int.TryParse(lblCustomerId.Text, out _customerId);

                        var customer = db.Customer_Master.FirstOrDefault(x => x.customerId == _customerId);

                        if (customer == null)
                        {
                            TxtCustomerName.SelectAll();
                            MessageBox.Show("'" + TxtCustomerName.Text + "' not found in vendor master", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void Autogenerate_InvoiceNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                var invoiceNumber = db.InvoiceHeaders.Max(m => m.invoiceNumber);

                int _invoiceNumber = 0;

                int.TryParse(invoiceNumber, out _invoiceNumber);

                TxtInvoiceNo.Text = string.Format("{0}", _invoiceNumber + 1);
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                var invoices = (from m in db.InvoiceHeaders
                                join v in db.Customer_Master on m.customerId equals v.customerId into ven
                                from v in ven.DefaultIfEmpty()
                                select new
                                {
                                    invoiceId = m.invoiceId,
                                    invoiceNumber = m.invoiceNumber,
                                    invoiceDate = m.invoiceDate,
                                    customerId = m.customerId,
                                    customerName = v.customerName,
                                    remark = m.remark,
                                    total = m.total,
                                    discount = m.discount,
                                    taxableAmt = m.taxableAmount,
                                    cgstPercent1 = m.cgstPercent1,
                                    cgstPercent2 = m.cgstPercent2,
                                    cgstAmount1 = m.cgstAmount1,
                                    cgstAmount2 = m.cgstAmount2,
                                    totalAmount = m.totalAmount
                                }).ToList();

                DgvList.DataSource = invoices;

                Set_Column_Purchase();
            }
        }

        private void Set_Column_Purchase()
        {
            DgvList.Columns["invoiceId"].Visible = false;
            DgvList.Columns["customerId"].Visible = false;

            DgvList.Columns["invoiceNumber"].HeaderText = "Invoice #";
            DgvList.Columns["invoiceNumber"].DisplayIndex = 0;

            DgvList.Columns["invoiceDate"].HeaderText = "Date";
            DgvList.Columns["invoiceDate"].DisplayIndex = 1;

            DgvList.Columns["customerName"].HeaderText = "Customer Name";
            DgvList.Columns["customerName"].DisplayIndex = 2;

            DgvList.Columns["remark"].HeaderText = "Remark";
            DgvList.Columns["remark"].DisplayIndex = 3;

            DgvList.Columns["total"].HeaderText = "Total Rs.";
            DgvList.Columns["total"].DisplayIndex = 4;

            DgvList.Columns["discount"].HeaderText = "Discount";
            DgvList.Columns["discount"].DisplayIndex = 5;

            DgvList.Columns["taxableAmt"].HeaderText = "Taxable Rs.";
            DgvList.Columns["taxableAmt"].DisplayIndex = 6;

            DgvList.Columns["cgstPercent1"].HeaderText = "Cgst %";
            DgvList.Columns["cgstPercent1"].DisplayIndex = 7;

            DgvList.Columns["cgstAmount1"].HeaderText = "Cgst Rs.";
            DgvList.Columns["cgstAmount1"].DisplayIndex = 8;

            DgvList.Columns["cgstPercent2"].HeaderText = "Cgst %";
            DgvList.Columns["cgstPercent2"].DisplayIndex = 9;

            DgvList.Columns["cgstAmount2"].HeaderText = "Cgst Rs.";
            DgvList.Columns["cgstAmount2"].DisplayIndex = 10;

            DgvList.Columns["totalAmount"].HeaderText = "Total Amount";
            DgvList.Columns["totalAmount"].DisplayIndex = 11;
        }

        private void Clear_Controls()
        {
            _invoiceId = 0;

            Autogenerate_InvoiceNumber();

            TxtCustomerName.Text = string.Empty;
            LstCustomer.Hide();

            DgvItems.Rows.Clear();

            lblCustomerId.Text = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtCustomerName.Text == string.Empty)
                {
                    MessageBox.Show("Customer Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            InvoiceHeader ih = new InvoiceHeader();

                            if (_invoiceId == 0)
                            {
                                db.InvoiceHeaders.Add(ih);

                                ih.invoiceNumber = TxtInvoiceNo.Text.Trim();
                                ih.entryBy = SessionHelper.UserId;
                                ih.entryDate = DateTime.Now;
                            }
                            else
                            {
                                ih = db.InvoiceHeaders.FirstOrDefault(m => m.invoiceId == _invoiceId);

                                ih.updatedBy = SessionHelper.UserId;
                                ih.updatedDate = DateTime.Now;
                            }

                            ih.invoiceDate = (DateTime)CommanMethods.ConvertDate(dtpDate.Text);
                            ih.remark = string.IsNullOrWhiteSpace(txtRemark.Text) ? null : txtRemark.Text;
                            ih.finYear = string.IsNullOrEmpty(SessionHelper.FinYear) ? null : SessionHelper.FinYear;

                            int _customerId = 0;
                            int.TryParse(lblCustomerId.Text, out _customerId);

                            ih.customerId = _customerId;

                            decimal _total = 0, _discount = 0, _taxableAmt = 0, _cgstPercent1 = 0, _cgstPercent2 = 0, _cgstAmt1 = 0, _cgstAmt2 = 0, _totalAmt = 0;

                            decimal.TryParse(Convert.ToString(TxtTotal.Text), out _total);
                            decimal.TryParse(Convert.ToString(TxtDiscount.Text), out _discount);
                            decimal.TryParse(Convert.ToString(TxtTaxableAmt.Text), out _taxableAmt);
                            decimal.TryParse(Convert.ToString(TxtCGST1.Text), out _cgstPercent1);
                            decimal.TryParse(Convert.ToString(TxtCGST2.Text), out _cgstPercent2);
                            decimal.TryParse(Convert.ToString(TxtCGST1Amt.Text), out _cgstAmt1);
                            decimal.TryParse(Convert.ToString(TxtCGST2Amt.Text), out _cgstAmt2);
                            decimal.TryParse(Convert.ToString(TxtTotalAmt.Text), out _totalAmt);

                            ih.total = _total == 0 ? null : (decimal?)_total;
                            ih.discount = _discount == 0 ? null : (decimal?)_discount;
                            ih.taxableAmount = _taxableAmt == 0 ? null : (decimal?)_taxableAmt;
                            ih.cgstPercent1 = _cgstPercent1 == 0 ? null : (decimal?)_cgstPercent1;
                            ih.cgstPercent2 = _cgstPercent2 == 0 ? null : (decimal?)_cgstPercent2;
                            ih.cgstAmount1 = _cgstAmt1 == 0 ? null : (decimal?)_cgstAmt1;
                            ih.cgstAmount2 = _cgstAmt2 == 0 ? null : (decimal?)_cgstAmt2;
                            ih.totalAmount = _totalAmt == 0 ? null : (decimal?)_totalAmt;

                            db.SaveChanges();

                            int _id = ih.invoiceId;

                            if (_id > 0)
                            {
                                InvoiceDetail id = new InvoiceDetail();

                                foreach (DataGridViewRow row in DgvItems.Rows)
                                {
                                    id = new InvoiceDetail();

                                    int _itemId = 0;

                                    int.TryParse(Convert.ToString(row.Cells["itemId"].Value), out _itemId);

                                    if (_itemId > 0)
                                    {
                                        int _invoiceDetailId = 0;

                                        int.TryParse(Convert.ToString(row.Cells["invoiceDetailId"].Value), out _invoiceDetailId);

                                        if (_invoiceDetailId == 0)
                                        {
                                            db.InvoiceDetails.Add(id);
                                        }
                                        else
                                        {
                                            id = db.InvoiceDetails.FirstOrDefault(x => x.invoiceDetailId == _invoiceDetailId);
                                        }

                                        decimal _rate = 0, _quantity = 0, _amount = 0;

                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Rate"].Value), out _rate);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Quantity"].Value), out _quantity);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Amount"].Value), out _amount);

                                        id.invoiceId = _id;
                                        id.itemId = _itemId;
                                        id.unit = string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_Unit"].Value)) ? null
                                                                        : Convert.ToString(row.Cells["Col_Unit"].Value);
                                        id.hsnCode = string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_HSNCode"].Value)) ? null
                                                                        : Convert.ToString(row.Cells["Col_HSNCode"].Value);
                                        id.rate = _rate == 0 ? null : (decimal?)_rate;
                                        id.quantity = _quantity == 0 ? null : (decimal?)_quantity;
                                        id.amount = _amount == 0 ? null : (decimal?)_amount;
                                    }
                                }

                                db.SaveChanges();
                            }

                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            throw;
                        }
                    }

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

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                Clear_Controls();

                using (db = new Inventory_DflowEntities())
                {
                    int _id = 0;

                    int.TryParse(Convert.ToString(DgvList["invoiceId", RowIndex].Value), out _id);

                    var ih = (from m in db.InvoiceHeaders
                              join v in db.Customer_Master on m.customerId equals v.customerId into ven
                              from v in ven.DefaultIfEmpty()
                              where m.invoiceId == _id
                              select new
                              {
                                  invoiceId = m.invoiceId,
                                  invoiceNumber = m.invoiceNumber,
                                  invoiceDate = m.invoiceDate,
                                  customerId = m.customerId,
                                  customerName = v.customerName,
                                  remark = m.remark,
                                  total = m.total,
                                  discount = m.discount,
                                  taxableAmt = m.taxableAmount,
                                  cgstPercent1 = m.cgstPercent1,
                                  cgstPercent2 = m.cgstPercent2,
                                  cgstAmount1 = m.cgstAmount1,
                                  cgstAmount2 = m.cgstAmount2,
                                  totalAmount = m.totalAmount
                              }).SingleOrDefault();

                    var pd = (from m in db.InvoiceDetails
                              join i in db.Item_Master on m.itemId equals i.itemId into item
                              from i in item.DefaultIfEmpty()
                              where m.invoiceId == _id
                              select new
                              {
                                  invoiceDetailId = m.invoiceDetailId,
                                  invoiceId = m.invoiceId,
                                  itemId = m.itemId,
                                  Col_Item = i.itemName,
                                  Col_HSNCode = m.hsnCode,
                                  Col_Unit = m.unit,
                                  Col_Rate = m.rate,
                                  Col_Quantity = m.quantity,
                                  Col_Amount = m.amount
                              }).ToList();

                    if (ih != null)
                    {
                        _invoiceId = ih.invoiceId;
                        TxtInvoiceNo.Text = ih.invoiceNumber;
                        dtpDate.Text = ih.invoiceDate.ToString("dd/MM/yyyy");
                        TxtCustomerName.Text = ih.customerName;
                        LstCustomer.Hide();
                        lblCustomerId.Text = Convert.ToString(ih.customerId);
                        txtRemark.Text = ih.remark;

                        if (pd != null)
                        {
                            foreach (var item in pd)
                            {
                                DgvItems.Rows.Add(item.Col_Item,
                                    item.Col_HSNCode,
                                    item.Col_Rate,
                                    item.Col_Quantity,
                                    item.Col_Amount,
                                    item.itemId,
                                    item.invoiceDetailId,
                                    item.Col_Unit);
                            }
                        }

                        TxtTotal.Text = Convert.ToString(ih.total);
                        TxtDiscount.Text = Convert.ToString(ih.discount);
                        TxtTaxableAmt.Text = Convert.ToString(ih.taxableAmt);
                        TxtCGST1.Text = Convert.ToString(ih.cgstPercent1);
                        TxtCGST2.Text = Convert.ToString(ih.cgstPercent2);
                        TxtCGST1Amt.Text = Convert.ToString(ih.cgstAmount1);
                        TxtCGST2Amt.Text = Convert.ToString(ih.cgstAmount2);
                        TxtTotalAmt.Text = Convert.ToString(ih.totalAmount);
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
                        if (DialogResult.No == MessageBox.Show("Are you sure to delete this record ?", "Warning", MessageBoxButtons.YesNo))
                            return;

                        int _id = 0;

                        int.TryParse(Convert.ToString(DgvList["invoiceId", DgvList.CurrentCell.RowIndex].Value), out _id);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                using (db = new Inventory_DflowEntities())
                                {
                                    var ih = db.InvoiceHeaders.Where(m => m.invoiceId == _id).FirstOrDefault();

                                    if (ih != null)
                                    {
                                        db.InvoiceDetails.Where(m => m.invoiceId == _id).ToList().ForEach(p => db.InvoiceDetails.Remove(p));

                                        db.InvoiceHeaders.Remove(ih);
                                    }

                                    db.SaveChanges();

                                    scope.Complete();
                                }
                            }
                            catch (Exception)
                            {
                                scope.Dispose();
                                throw;
                            }
                        }

                        Get_Data();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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

        private void TxtCGST1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calculate_Total_Amount();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtCGST2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calculate_Total_Amount();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calculate_Total_Amount();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DgvItems_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtDiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal _discount = 0;

                decimal.TryParse(TxtDiscount.Text, out _discount);

                TxtDiscount.Text = string.Format("{0:0.00}", _discount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtCGST1_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal _gst = 0;

                decimal.TryParse(TxtCGST1.Text, out _gst);

                TxtCGST1.Text = string.Format("{0:0.00}", _gst);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtCGST2_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal _gst = 0;

                decimal.TryParse(TxtCGST2.Text, out _gst);

                TxtCGST2.Text = string.Format("{0:0.00}", _gst);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DgvItems.CurrentCell != null)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        using (db = new Inventory_DflowEntities())
                        {
                            using (var scope = new TransactionScope())
                            {
                                try
                                {
                                    int rowIndex = DgvItems.CurrentCell.RowIndex;

                                    int _invoiceDetailId = 0;

                                    int.TryParse(Convert.ToString(DgvItems["invoiceDetailId", rowIndex].Value), out _invoiceDetailId);

                                    DgvItems.Rows.RemoveAt(rowIndex);

                                    Calculate_Total_Amount();

                                    var id = db.InvoiceDetails.FirstOrDefault(m => m.invoiceDetailId == _invoiceDetailId);

                                    if (id != null)
                                    {
                                        db.InvoiceDetails.Remove(id);

                                        db.SaveChanges();

                                        var ih = db.InvoiceHeaders.FirstOrDefault(x => x.invoiceId == id.invoiceId);

                                        if (ih != null)
                                        {
                                            decimal _total = 0, _discount = 0, _taxableAmt = 0, _cgstPercent1 = 0, _cgstPercent2 = 0, _cgstAmt1 = 0,
                                                    _cgstAmt2 = 0, _totalAmt = 0;

                                            decimal.TryParse(Convert.ToString(TxtTotal.Text), out _total);
                                            decimal.TryParse(Convert.ToString(TxtDiscount.Text), out _discount);
                                            decimal.TryParse(Convert.ToString(TxtTaxableAmt.Text), out _taxableAmt);
                                            decimal.TryParse(Convert.ToString(TxtCGST1.Text), out _cgstPercent1);
                                            decimal.TryParse(Convert.ToString(TxtCGST2.Text), out _cgstPercent2);
                                            decimal.TryParse(Convert.ToString(TxtCGST1Amt.Text), out _cgstAmt1);
                                            decimal.TryParse(Convert.ToString(TxtCGST2Amt.Text), out _cgstAmt2);
                                            decimal.TryParse(Convert.ToString(TxtTotalAmt.Text), out _totalAmt);

                                            ih.total = _total == 0 ? null : (decimal?)_total;
                                            ih.discount = _discount == 0 ? null : (decimal?)_discount;
                                            ih.taxableAmount = _taxableAmt == 0 ? null : (decimal?)_taxableAmt;
                                            ih.cgstPercent1 = _cgstPercent1 == 0 ? null : (decimal?)_cgstPercent1;
                                            ih.cgstPercent2 = _cgstPercent2 == 0 ? null : (decimal?)_cgstPercent2;
                                            ih.cgstAmount1 = _cgstAmt1 == 0 ? null : (decimal?)_cgstAmt1;
                                            ih.cgstAmount2 = _cgstAmt2 == 0 ? null : (decimal?)_cgstAmt2;
                                            ih.totalAmount = _totalAmt == 0 ? null : (decimal?)_totalAmt;

                                            db.SaveChanges();
                                        }
                                    }

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
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
