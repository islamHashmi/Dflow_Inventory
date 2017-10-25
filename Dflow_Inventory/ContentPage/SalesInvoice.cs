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

        public int InvoiceId { get => _invoiceId; set => _invoiceId = value; }

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
                    if (db.Item_Master.Where(x => x.active == true) != null)
                    {
                        foreach (var item in db.Item_Master.Where(x => x.active == true))
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

                if (e.Control is TextBox txtItemName)
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
                if (e.Control is TextBox tb)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Decimal_KeyPress);
                }
            }

            if (DgvItems.CurrentCell.ColumnIndex == 3)
            {
                if (e.Control is TextBox tb)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Integer_KeyPress);
                }
            }

            if (DgvItems.CurrentCell.ColumnIndex == 4)
            {
                if (e.Control is TextBox tb)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    tb.KeyPress += new KeyPressEventHandler(Column_Decimal_KeyPress);
                }
            }
        }

        private void Column_Integer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Column_Decimal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    string itemName = Convert.ToString(DgvItems[e.ColumnIndex, e.RowIndex].Value);

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
                            DgvItems["Col_HSNCode", e.RowIndex].Value = SessionHelper.HsnCode;
                        }
                        else
                        {
                            DgvItems["itemId", e.RowIndex].Value = DBNull.Value;
                            DgvItems["Col_Unit", e.RowIndex].Value = DBNull.Value;
                            DgvItems["Col_HSNCode", e.RowIndex].Value = DBNull.Value;
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
            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Rate", RowIndex].Value), result: out decimal _rate);
            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Quantity", RowIndex].Value), result: out decimal _quantity);
            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Amount", RowIndex].Value), result: out decimal _amount);

            _amount = _rate * _quantity;

            DgvItems["Col_Rate", RowIndex].Value = $"{_rate:0.00}";
            DgvItems["Col_Amount", RowIndex].Value = $"{_amount:0.00}";

            Calculate_Total_Amount();
        }

        private void Calculate_Total_Amount()
        {
            decimal _totalAmount = 0;

            foreach (DataGridViewRow row in DgvItems.Rows)
            {
                decimal.TryParse(Convert.ToString(value: row.Cells["Col_Amount"].Value), result: out decimal _amount);

                _totalAmount += _amount;
            }

            TxtTotal.Text = $"{_totalAmount:0.00}";

            decimal _taxableAmt = 0, _cgst1 = 0, _sgst2 = 0, _totalAmt = 0;

            decimal.TryParse(TxtDiscount.Text, out decimal _discount);

            _taxableAmt = _totalAmount - _discount;

            TxtTaxableAmt.Text = $"{_taxableAmt:0.00}";

            decimal _gstPercent1 = 0, _gstPercent2 = 0;

            decimal.TryParse(TxtCGST.Text, out _gstPercent1);
            decimal.TryParse(TxtSGST.Text, out _gstPercent2);

            _cgst1 = (_taxableAmt * _gstPercent1) / 100;
            _sgst2 = (_taxableAmt * _gstPercent2) / 100;

            TxtCGSTAmt.Text = $"{_cgst1:0.00}";

            TxtSGSTAmt.Text = $"{_sgst2:0.00}";

            _totalAmt = _taxableAmt + _cgst1 + _sgst2;

            TxtTotalAmt.Text = $"{_totalAmt:0.00}";
        }

        private void TxtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    if (db.Customer_Master
                                    .Select(m => new
                                    {
                                        vendorId = m.customerId,
                                        vendorName = m.customerName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtCustomerName.Text.Trim()))
                                    .ToList() != null)
                    {
                        LstCustomer.DataSource = db.Customer_Master
                                    .Select(m => new
                                    {
                                        vendorId = m.customerId,
                                        vendorName = m.customerName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtCustomerName.Text.Trim()))
                                    .ToList();
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

        private void Autogenerate_InvoiceNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(db.InvoiceHeaders.Max(m => m.invoiceNumber), out int _invoiceNumber);

                TxtInvoiceNo.Text = $"{_invoiceNumber + 1}";
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from m in db.InvoiceHeaders
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
                                          cgstPercent = m.cgstPercent,
                                          sgstPercent = m.sgstPercent,
                                          cgstAmount = m.cgstAmount,
                                          sgstAmount = m.sgstAmount,
                                          totalAmount = m.totalAmount
                                      }).ToList();

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

            DgvList.Columns["cgstPercent"].HeaderText = "CGST %";
            DgvList.Columns["cgstPercent"].DisplayIndex = 7;

            DgvList.Columns["cgstAmount"].HeaderText = "CGST Rs.";
            DgvList.Columns["cgstAmount"].DisplayIndex = 8;

            DgvList.Columns["sgstPercent"].HeaderText = "SGST %";
            DgvList.Columns["sgstPercent"].DisplayIndex = 9;

            DgvList.Columns["sgstAmount"].HeaderText = "SGST Rs.";
            DgvList.Columns["sgstAmount"].DisplayIndex = 10;

            DgvList.Columns["totalAmount"].HeaderText = "Total Amount";
            DgvList.Columns["totalAmount"].DisplayIndex = 11;
        }

        private void Clear_Controls()
        {
            InvoiceId = 0;

            Autogenerate_InvoiceNumber();

            TxtTotal.Text = string.Empty;
            TxtDiscount.Text = string.Empty;
            TxtTaxableAmt.Text = string.Empty;
            TxtCGST.Text = string.Empty;
            TxtCGSTAmt.Text = string.Empty;
            TxtSGST.Text = string.Empty;
            TxtSGSTAmt.Text = string.Empty;
            TxtTotalAmt.Text = string.Empty;
            txtRemark.Text = string.Empty;

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

                            if (InvoiceId == 0)
                            {
                                db.InvoiceHeaders.Add(ih);

                                ih.invoiceNumber = TxtInvoiceNo.Text.Trim();
                                ih.entryBy = SessionHelper.UserId;
                                ih.entryDate = DateTime.Now;
                            }
                            else
                            {
                                ih = db.InvoiceHeaders.FirstOrDefault(m => m.invoiceId == InvoiceId);

                                ih.updatedBy = SessionHelper.UserId;
                                ih.updatedDate = DateTime.Now;
                            }

                            ih.invoiceDate = (DateTime)CommanMethods.ConvertDate(dtpDate.Text);
                            ih.remark = string.IsNullOrWhiteSpace(txtRemark.Text) ? null : txtRemark.Text;
                            ih.finYear = string.IsNullOrEmpty(SessionHelper.FinYear) ? null : SessionHelper.FinYear;

                            int.TryParse(lblCustomerId.Text, out int _customerId);

                            ih.customerId = _customerId;

                            decimal.TryParse(Convert.ToString(TxtTotal.Text), out decimal _total);
                            decimal.TryParse(Convert.ToString(TxtDiscount.Text), out decimal _discount);
                            decimal.TryParse(Convert.ToString(TxtTaxableAmt.Text), out decimal _taxableAmt);
                            decimal.TryParse(Convert.ToString(TxtCGST.Text), out decimal _cgstPercent);
                            decimal.TryParse(Convert.ToString(TxtSGST.Text), out decimal _sgstPercent);
                            decimal.TryParse(Convert.ToString(TxtCGSTAmt.Text), out decimal _sgstAmt);
                            decimal.TryParse(Convert.ToString(TxtSGSTAmt.Text), out decimal _cgstAmt);
                            decimal.TryParse(Convert.ToString(TxtTotalAmt.Text), out decimal _totalAmt);

                            ih.total = _total == 0 ? null : (decimal?)_total;
                            ih.discount = _discount == 0 ? null : (decimal?)_discount;
                            ih.taxableAmount = _taxableAmt == 0 ? null : (decimal?)_taxableAmt;
                            ih.cgstPercent = _cgstPercent == 0 ? null : (decimal?)_cgstPercent;
                            ih.sgstPercent = _sgstPercent == 0 ? null : (decimal?)_sgstPercent;
                            ih.cgstAmount = _cgstAmt == 0 ? null : (decimal?)_cgstAmt;
                            ih.sgstAmount = _sgstAmt == 0 ? null : (decimal?)_sgstAmt;
                            ih.totalAmount = _totalAmt == 0 ? null : (decimal?)_totalAmt;

                            db.SaveChanges();

                            int _id = ih.invoiceId;

                            if (_id > 0)
                            {
                                InvoiceDetail id = new InvoiceDetail();

                                foreach (DataGridViewRow row in DgvItems.Rows)
                                {
                                    id = new InvoiceDetail();

                                    int.TryParse(Convert.ToString(row.Cells["itemId"].Value), out int _itemId);

                                    if (_itemId > 0)
                                    {
                                        string cmdType = string.Empty;

                                        int.TryParse(Convert.ToString(value: row.Cells["invoiceDetailId"].Value), result: out int _invoiceDetailId);

                                        if (_invoiceDetailId == 0)
                                        {
                                            db.InvoiceDetails.Add(id);
                                            cmdType = "I";
                                        }
                                        else
                                        {
                                            id = db.InvoiceDetails.FirstOrDefault(x => x.invoiceDetailId == _invoiceDetailId);
                                            cmdType = "U";
                                        }

                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Rate"].Value), out decimal _rate);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Quantity"].Value), out decimal _quantity);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Amount"].Value), out decimal _amount);

                                        id.invoiceId = _id;
                                        id.itemId = _itemId;
                                        id.unit = string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_Unit"].Value)) ? null
                                                                        : Convert.ToString(row.Cells["Col_Unit"].Value);
                                        id.hsnCode = string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_HSNCode"].Value)) ? null
                                                                        : Convert.ToString(row.Cells["Col_HSNCode"].Value);
                                        id.rate = _rate == 0 ? null : (decimal?)_rate;
                                        id.quantity = _quantity == 0 ? null : (decimal?)_quantity;
                                        id.amount = _amount == 0 ? null : (decimal?)_amount;

                                        db.sp_Stock_InsertUpdate(CommanMethods.ConvertDate(dtpDate.Text), _itemId, "S", _quantity == 0 ? null : (decimal?)_quantity, _id, null, cmdType, SessionHelper.UserId);
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
                    int.TryParse(Convert.ToString(DgvList["invoiceId", RowIndex].Value), out int _id);

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
                                  cgstPercent = m.cgstPercent,
                                  sgstPercent = m.sgstPercent,
                                  cgstAmount = m.cgstAmount,
                                  sgstAmount = m.sgstAmount,
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
                        InvoiceId = ih.invoiceId;
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
                        TxtCGST.Text = Convert.ToString(ih.cgstPercent);
                        TxtSGST.Text = Convert.ToString(ih.sgstPercent);
                        TxtCGSTAmt.Text = Convert.ToString(ih.cgstAmount);
                        TxtSGSTAmt.Text = Convert.ToString(ih.sgstAmount);
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
                        
                        int.TryParse(Convert.ToString(DgvList["invoiceId", DgvList.CurrentCell.RowIndex].Value), out int _id);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                using (db = new Inventory_DflowEntities())
                                {
                                    if (db.InvoiceHeaders.Where(m => m.invoiceId == _id).FirstOrDefault() != null)
                                    {
                                        db.InvoiceDetails.Where(m => m.invoiceId == _id).ToList().ForEach(p => db.InvoiceDetails.Remove(p));

                                        db.InvoiceHeaders.Remove(db.InvoiceHeaders.Where(m => m.invoiceId == _id).FirstOrDefault());
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
        
        private void TxtDiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.TryParse(TxtDiscount.Text, out decimal _discount);

                TxtDiscount.Text = $"{_discount:0.00}";
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
                                    int.TryParse(Convert.ToString(DgvItems["invoiceDetailId", DgvItems.CurrentCell.RowIndex].Value), out int _invoiceDetailId);

                                    DgvItems.Rows.RemoveAt(DgvItems.CurrentCell.RowIndex);

                                    Calculate_Total_Amount();


                                    if (db.InvoiceDetails.FirstOrDefault(m => m.invoiceDetailId == _invoiceDetailId) != null)
                                    {
                                        db.InvoiceDetails.Remove(db.InvoiceDetails.FirstOrDefault(m => m.invoiceDetailId == _invoiceDetailId));

                                        db.SaveChanges();

                                        var ih = db.InvoiceHeaders.FirstOrDefault(x => x.invoiceId == db.InvoiceDetails.FirstOrDefault(m => m.invoiceDetailId == _invoiceDetailId).invoiceId);

                                        if (ih != null)
                                        {
                                            decimal.TryParse(Convert.ToString(TxtTotal.Text), out decimal _total);
                                            decimal.TryParse(Convert.ToString(TxtDiscount.Text), out decimal _discount);
                                            decimal.TryParse(Convert.ToString(TxtTaxableAmt.Text), out decimal _taxableAmt);
                                            decimal.TryParse(Convert.ToString(TxtCGST.Text), out decimal _cgstPercent);
                                            decimal.TryParse(Convert.ToString(TxtSGST.Text), out decimal _sgstPercent);
                                            decimal.TryParse(Convert.ToString(TxtCGSTAmt.Text), out decimal _cgstAmt);
                                            decimal.TryParse(Convert.ToString(TxtSGSTAmt.Text), out decimal _sgstAmt);
                                            decimal.TryParse(Convert.ToString(TxtTotalAmt.Text), out decimal _totalAmt);

                                            ih.total = _total == 0 ? null : (decimal?)_total;
                                            ih.discount = _discount == 0 ? null : (decimal?)_discount;
                                            ih.taxableAmount = _taxableAmt == 0 ? null : (decimal?)_taxableAmt;
                                            ih.cgstPercent = _cgstPercent == 0 ? null : (decimal?)_cgstPercent;
                                            ih.sgstPercent = _sgstPercent == 0 ? null : (decimal?)_sgstPercent;
                                            ih.cgstAmount = _cgstAmt == 0 ? null : (decimal?)_cgstAmt;
                                            ih.sgstAmount = _sgstAmt == 0 ? null : (decimal?)_sgstAmt;
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

        private void TxtCGST_TextChanged(object sender, EventArgs e)
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

        private void TxtCGST_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.TryParse(TxtCGST.Text, out decimal _gst);

                TxtCGST.Text = $"{_gst:0.00}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtSGST_TextChanged(object sender, EventArgs e)
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

        private void TxtSGST_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.TryParse(TxtSGST.Text, out decimal _gst);

                TxtSGST.Text = $"{_gst:0.00}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtCustomerName_KeyDown_1(object sender, KeyEventArgs e)
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

        private void DgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (Convert.ToString(e.FormattedValue) != string.Empty)
                    {
                        foreach (DataGridViewRow row in DgvItems.Rows)
                        {
                            if (Convert.ToString(row.Cells["Col_Item"].Value) == Convert.ToString(e.FormattedValue) && row.Index != e.RowIndex)
                            {
                                MessageBox.Show("Duplicate Item Name : " + Convert.ToString(e.FormattedValue), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                                return;
                            }
                        }

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.Item_Master.FirstOrDefault(m => m.itemName == Convert.ToString(e.FormattedValue)) == null)
                            {
                                MessageBox.Show("Not found in master Item Name : " + Convert.ToString(e.FormattedValue), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                }

                if (e.ColumnIndex == 3)
                {
                    int.TryParse(Convert.ToString(DgvItems["ItemId", e.RowIndex].Value), out int _itemId);
                    decimal.TryParse(Convert.ToString(e.FormattedValue), out decimal _quantity);

                    using (db = new Inventory_DflowEntities())
                    {
                        if (db.Item_Master.FirstOrDefault(m => m.itemId == _itemId) != null)
                        {
                            if (db.Item_Master.FirstOrDefault(m => m.itemId == _itemId).currentStock < _quantity)
                            {
                                MessageBox.Show("Entered quantity (" + _quantity + ") exceeds stock quantity (" + db.Item_Master.FirstOrDefault(m => m.itemId == _itemId).currentStock + ")", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
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

        private void DgvItems_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(DgvItems["Col_Item", e.RowIndex].Value)))
                {
                    decimal.TryParse(s: Convert.ToString(DgvItems["Col_Rate", e.RowIndex].Value), result: out decimal rate);
                    decimal.TryParse(s: Convert.ToString(DgvItems["Col_Quantity", e.RowIndex].Value), result: out decimal quantity);
                    decimal.TryParse(s: Convert.ToString(DgvItems["Col_Amount", e.RowIndex].Value), result: out decimal amount);

                    if (rate <= 0)
                    {
                        MessageBox.Show(text: "Rate is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, caption: "Information", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                    else if (quantity <= 0)
                    {
                        MessageBox.Show(text: "Quantity is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, caption: "Information", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                    else if (amount <= 0)
                    {
                        MessageBox.Show(text: "Amount is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, caption: "Information", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                        e.Cancel = true;
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
