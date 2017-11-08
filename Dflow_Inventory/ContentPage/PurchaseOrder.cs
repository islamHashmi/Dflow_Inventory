using System.Drawing;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Linq;
using System;
using System.Transactions;

namespace Dflow_Inventory.ContentPage
{
    public partial class PurchaseOrder : Form
    {
        private Inventory_DflowEntities db;

        private int _purchaseId = 0;

        public int PurchaseId { get => _purchaseId; set => _purchaseId = value; }

        public PurchaseOrder()
        {
            InitializeComponent();

            Autogenerate_PoNumber();
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
                    if (db.ItemMasters.Where(x => x.active == true) != null)
                    {
                        foreach (var item in db.ItemMasters.Where(x => x.active == true))
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

                    string[] split = itemName.Split('-');

                    string itemCode = itemName;

                    using (db = new Inventory_DflowEntities())
                    {
                        var item = (from m in db.ItemMasters
                                    join b in db.UnitMasters on m.unitId equals b.unitId into unit
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
                Calculate_Total_Amount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Calculate_Amount(int ColumnIndex, int RowIndex)
        {
            decimal _rate = 0, _quantity = 0, _amount = 0;

            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Rate", RowIndex].Value), result: out _rate);
            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Quantity", RowIndex].Value), result: out _quantity);
            decimal.TryParse(Convert.ToString(value: DgvItems["Col_Amount", RowIndex].Value), result: out _amount);

            _amount = _rate * _quantity;

            DgvItems["Col_Amount", RowIndex].Value = _amount;

        }

        private void Calculate_Total_Amount()
        {
            decimal _amount = 0, _totalAmount = 0;

            foreach (DataGridViewRow row in DgvItems.Rows)
            {
                decimal.TryParse(Convert.ToString(value: row.Cells["Col_Amount"].Value), result: out _amount);

                _totalAmount += _amount;
            }

            lblTotalAmount.Text = $"{_totalAmount:0.00}";
        }

        private void TxtVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {

                    if (db.VendorMasters
                                    .Select(m => new
                                    {
                                        vendorId = m.vendorId,
                                        vendorName = m.vendorName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtVendorName.Text.Trim()))
                                    .ToList() != null)
                    {
                        LstVendor.DataSource = db.VendorMasters
                                    .Select(m => new
                                    {
                                        vendorId = m.vendorId,
                                        vendorName = m.vendorName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtVendorName.Text.Trim()))
                                    .ToList();
                        LstVendor.DisplayMember = "vendorName";
                        LstVendor.ValueMember = "vendorId";
                        LstVendor.SelectedIndex = -1;
                    }
                }

                LstVendor_Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LstVendor_Show()
        {
            LstVendor.Show();
            LstVendor.BringToFront();
            LstVendor.Location = new Point(TxtVendorName.Location.X, TxtVendorName.Location.Y + 25);
            LstVendor.Width = TxtVendorName.Width;
            LstVendor.Height = 200;
        }

        private void TxtVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    LstVendor_Show();

                    LstVendor.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LstVendor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string referredById = LstVendor.SelectedValue.ToString();

                    TxtVendorName.Text = LstVendor.Text;

                    lblVendorId.Text = referredById;

                    LstVendor.Hide();

                    TxtVendorName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LstVendor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string referredById = LstVendor.SelectedValue.ToString();

                TxtVendorName.Text = LstVendor.Text;

                lblVendorId.Text = referredById;

                LstVendor.Hide();

                TxtVendorName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LstVendor_Leave(object sender, EventArgs e)
        {
            try
            {
                LstVendor.Hide();
                TxtVendorName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtVendorName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtVendorName.Text))
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        int _vendorId = 0;

                        int.TryParse(lblVendorId.Text, out _vendorId);

                        if (db.VendorMasters.FirstOrDefault(x => x.vendorId == _vendorId) == null)
                        {
                            TxtVendorName.SelectAll();
                            MessageBox.Show("'" + TxtVendorName.Text + "' not found in vendor master", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Autogenerate_PoNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(s: db.PurchaseHeaders.Max(m => m.poNumber), result: out int _poNumber);

                TxtPONumber.Text = $"{_poNumber + 1}";
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {

                DgvList.DataSource = (from m in db.PurchaseHeaders
                                      join v in db.VendorMasters on m.vendorId equals v.vendorId into ven
                                      from v in ven.DefaultIfEmpty()
                                      select new
                                      {
                                          purchaseId = m.purchaseId,
                                          poNumber = m.poNumber,
                                          purchaseDate = m.purchaseDate,
                                          orderNumber = m.orderNumber,
                                          orderDate = m.orderDate,
                                          vendorId = m.vendorId,
                                          vendorName = v.vendorName,
                                          totalQuantity = m.totalQuantity,
                                          totalAmount = m.totalAmount
                                      }).ToList();

                Set_Column_Purchase();
            }
        }

        private void Clear_Controls()
        {
            PurchaseId = 0;

            Autogenerate_PoNumber();

            TxtVendorName.Text = string.Empty;
            TxtOrderNo.Text = string.Empty;
            LstVendor.Hide();

            DgvItems.Rows.Clear();

            lblVendorId.Text = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtVendorName.Text == string.Empty)
                {
                    MessageBox.Show("Vendor Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            PurchaseHeader ph = new PurchaseHeader();

                            if (PurchaseId == 0)
                            {
                                db.PurchaseHeaders.Add(ph);

                                ph.poNumber = TxtPONumber.Text.Trim();
                                ph.entryBy = SessionHelper.UserId;
                                ph.entryDate = DateTime.Now;
                            }
                            else
                            {
                                ph = db.PurchaseHeaders.FirstOrDefault(m => m.purchaseId == PurchaseId);

                                ph.updatedBy = SessionHelper.UserId;
                                ph.updatedDate = DateTime.Now;
                            }

                            ph.purchaseDate = (DateTime)CommanMethods.ConvertDate(dtpDate.Text);
                            ph.orderNumber = string.IsNullOrEmpty(TxtOrderNo.Text) ? null : TxtOrderNo.Text;
                            ph.orderDate = CommanMethods.ConvertDate(dtpOrderDate.Text);
                            ph.remark = string.IsNullOrWhiteSpace(txtRemark.Text) ? null : txtRemark.Text;
                            ph.finYear = string.IsNullOrEmpty(SessionHelper.FinYear) ? null : SessionHelper.FinYear;

                            int.TryParse(lblVendorId.Text, out int _vendorId);

                            ph.vendorId = _vendorId;

                            db.SaveChanges();

                            int _id = ph.purchaseId;

                            if (_id > 0)
                            {
                                PurchaseDetail pd = new PurchaseDetail();

                                foreach (DataGridViewRow row in DgvItems.Rows)
                                {
                                    pd = new PurchaseDetail();

                                    int.TryParse(Convert.ToString(row.Cells["itemId"].Value), out int _itemId);

                                    if (_itemId > 0)
                                    {
                                        string cmdType = string.Empty;

                                        int.TryParse(Convert.ToString(row.Cells["purchaseDetailId"].Value), out int _purchaseDetailId);

                                        if (_purchaseDetailId == 0)
                                        {
                                            db.PurchaseDetails.Add(pd);
                                            cmdType = "I";
                                        }
                                        else
                                        {
                                            pd = db.PurchaseDetails.FirstOrDefault(x => x.purchaseDetailId == _purchaseDetailId);
                                            cmdType = "U";
                                        }
                                        
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Rate"].Value), out decimal _rate);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Quantity"].Value), out decimal _quantity);
                                        decimal.TryParse(Convert.ToString(row.Cells["Col_Amount"].Value), out decimal _amount);

                                        pd.purhcaseId = _id;
                                        pd.itemId = _itemId;
                                        pd.unit = string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_Unit"].Value)) ? null
                                                                        : Convert.ToString(row.Cells["Col_Unit"].Value);
                                        pd.rate = _rate == 0 ? null : (decimal?)_rate;
                                        pd.quantity = _quantity == 0 ? null : (decimal?)_quantity;
                                        pd.amount = _amount == 0 ? null : (decimal?)_amount;

                                        db.sp_Stock_InsertUpdate(CommanMethods.ConvertDate(dtpDate.Text), _itemId, "R",
                                                                    _quantity == 0 ? null : (decimal?)_quantity, null, _id, null, cmdType, SessionHelper.UserId);
                                    }
                                }

                                db.SaveChanges();

                                var totals = db.PurchaseDetails
                                        .Where(m => m.purhcaseId == _id)
                                        .GroupBy(m => m.purhcaseId)
                                        .Select(m => new
                                        {
                                            totalQty = m.Sum(x => x.quantity),
                                            totalAmt = m.Sum(x => x.amount)
                                        }).SingleOrDefault();

                                if (totals != null)
                                {
                                    ph = db.PurchaseHeaders.FirstOrDefault(x => x.purchaseId == _id);

                                    if (ph != null)
                                    {
                                        ph.totalQuantity = totals.totalQty;
                                        ph.totalAmount = totals.totalAmt;
                                    }

                                    db.SaveChanges();
                                }
                            }

                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            MessageBox.Show(ex.Message);
                        }
                    }

                    Clear_Controls();

                    Get_Data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void Set_Column_Purchase()
        {
            DgvList.Columns["purchaseId"].Visible = false;
            DgvList.Columns["vendorId"].Visible = false;

            DgvList.Columns["poNumber"].HeaderText = "P.O. Number";
            DgvList.Columns["poNumber"].DisplayIndex = 0;

            DgvList.Columns["purchaseDate"].HeaderText = "Date";
            DgvList.Columns["purchaseDate"].DisplayIndex = 1;

            DgvList.Columns["orderNumber"].HeaderText = "Order No.";
            DgvList.Columns["orderNumber"].DisplayIndex = 2;

            DgvList.Columns["orderDate"].HeaderText = "Order Date";
            DgvList.Columns["orderDate"].DisplayIndex = 3;

            DgvList.Columns["vendorName"].HeaderText = "Vendor Name";
            DgvList.Columns["vendorName"].DisplayIndex = 4;

            DgvList.Columns["totalQuantity"].HeaderText = "Total Qty.";
            DgvList.Columns["totalQuantity"].DisplayIndex = 5;

            DgvList.Columns["totalAmount"].HeaderText = "Total Amt.";
            DgvList.Columns["totalAmount"].DisplayIndex = 6;
        }

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                Clear_Controls();

                using (db = new Inventory_DflowEntities())
                {
                    int.TryParse(s: Convert.ToString(DgvList["purchaseId", RowIndex].Value), result: out int _id);

                    var ph = (from m in db.PurchaseHeaders
                              join v in db.VendorMasters on m.vendorId equals v.vendorId into ven
                              from v in ven.DefaultIfEmpty()
                              where m.purchaseId == _id
                              select new
                              {
                                  purchaseId = m.purchaseId,
                                  poNumber = m.poNumber,
                                  purchaseDate = m.purchaseDate,
                                  orderNumber = m.orderNumber,
                                  orderDate = m.orderDate,
                                  vendorId = m.vendorId,
                                  vendorName = v.vendorName,
                                  totalQuantity = m.totalQuantity,
                                  totalAmount = m.totalAmount,
                                  remark = m.remark
                              }).SingleOrDefault();

                    var pd = (from m in db.PurchaseDetails
                              join i in db.ItemMasters on m.itemId equals i.itemId into item
                              from i in item.DefaultIfEmpty()
                              where m.purhcaseId == _id
                              select new
                              {
                                  purchaseDetailId = m.purchaseDetailId,
                                  purchaseId = m.purhcaseId,
                                  itemId = m.itemId,
                                  Col_Item = i.itemName,
                                  Col_Unit = m.unit,
                                  Col_Rate = m.rate,
                                  Col_Quantity = m.quantity,
                                  Col_Amount = m.amount
                              }).ToList();

                    if (ph != null)
                    {
                        PurchaseId = ph.purchaseId;
                        TxtPONumber.Text = ph.poNumber;
                        dtpDate.Text = ph.purchaseDate.ToString("dd/MM/yyyy");
                        TxtOrderNo.Text = ph.orderNumber;
                        dtpOrderDate.Text = ph.orderDate == null ? DateTime.Now.ToString("dd/MM/yyyy") : Convert.ToDateTime(ph.orderDate).ToString("dd/MM/yyyy");
                        TxtVendorName.Text = ph.vendorName;
                        LstVendor.Hide();
                        lblVendorId.Text = Convert.ToString(ph.vendorId);
                        txtRemark.Text = ph.remark;

                        if (pd != null)
                        {
                            foreach (var item in pd)
                            {
                                DgvItems.Rows.Add(item.Col_Item,
                                                item.Col_Unit,
                                                item.Col_Rate,
                                                item.Col_Quantity,
                                                item.Col_Amount,
                                                item.itemId,
                                                item.purchaseDetailId);
                            }
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
                        if (DialogResult.No == MessageBox.Show("Are you sure to delete this record ?", "Warning", MessageBoxButtons.YesNo))
                            return;


                        int.TryParse(Convert.ToString(DgvList["purchaseId", DgvList.CurrentCell.RowIndex].Value), out int _id);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                using (db = new Inventory_DflowEntities())
                                {
                                    if (db.PurchaseHeaders.Where(m => m.purchaseId == _id).FirstOrDefault() != null)
                                    {
                                        db.PurchaseDetails.Where(m => m.purhcaseId == _id).ToList().ForEach(p => db.PurchaseDetails.Remove(p));

                                        db.PurchaseHeaders.Remove(db.PurchaseHeaders.Where(m => m.purchaseId == _id).FirstOrDefault());
                                    }

                                    db.SaveChanges();

                                    scope.Complete();
                                }
                            }
                            catch (Exception ex)
                            {
                                scope.Dispose();
                                MessageBox.Show(ex.Message);
                            }
                        }

                        Get_Data();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void DgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
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

                                int.TryParse(Convert.ToString(DgvItems["purchaseDetailId", rowIndex].Value), out int _purchaseDetailId);

                                DgvItems.Rows.RemoveAt(rowIndex);

                                if (db.PurchaseDetails.FirstOrDefault(m => m.purchaseDetailId == _purchaseDetailId) != null)
                                {
                                    db.PurchaseDetails.Remove(db.PurchaseDetails.FirstOrDefault(m => m.purchaseDetailId == _purchaseDetailId));

                                    db.SaveChanges();

                                    var totals = db.PurchaseDetails
                                                .Where(m => m.purhcaseId == db.PurchaseDetails.FirstOrDefault(p => p.purchaseDetailId == _purchaseDetailId).purhcaseId)
                                                .GroupBy(m => m.purhcaseId)
                                                .Select(m => new
                                                {
                                                    totalQty = m.Sum(x => x.quantity),
                                                    totalAmt = m.Sum(x => x.amount)
                                                }).SingleOrDefault();

                                    if (db.PurchaseDetails.FirstOrDefault(m => m.purchaseDetailId == _purchaseDetailId) != null)
                                    {
                                        if (totals != null)
                                        {
                                            var ph = db.PurchaseHeaders.FirstOrDefault(x => x.purchaseId == db.PurchaseDetails.FirstOrDefault(m => m.purchaseDetailId == _purchaseDetailId).purhcaseId);

                                            if (ph != null)
                                            {
                                                ph.totalQuantity = totals.totalQty;
                                                ph.totalAmount = totals.totalAmt;
                                            }

                                            db.SaveChanges();
                                        }
                                    }
                                }

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                            DataContext.ItemMaster item = ItemDetails(Convert.ToString(e.FormattedValue));

                            if (item == null)
                            {
                                MessageBox.Show("Not found in master Item Name : " + Convert.ToString(e.FormattedValue), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataContext.ItemMaster ItemDetails(string itemName)
        {
            return db.ItemMasters.FirstOrDefault(m => m.itemName == itemName);
        }

        private void DgvItems_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(DgvItems["Col_Item", e.RowIndex].Value)))
                {
                    decimal.TryParse(Convert.ToString(value: DgvItems["Col_Rate", e.RowIndex].Value), result: out decimal rate);
                    decimal.TryParse(Convert.ToString(value: DgvItems["Col_Quantity", e.RowIndex].Value), result: out decimal quantity);
                    decimal.TryParse(Convert.ToString(value: DgvItems["Col_Amount", e.RowIndex].Value), result: out decimal amount);

                    if (rate <= 0)
                    {
                        MessageBox.Show("Rate is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                    else if (quantity <= 0)
                    {
                        MessageBox.Show("Quantity is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                    else if (amount <= 0)
                    {
                        MessageBox.Show("Amount is empty for Item :" + DgvItems["Col_Item", e.RowIndex].Value, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
