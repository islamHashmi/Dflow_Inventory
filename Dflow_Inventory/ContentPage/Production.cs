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
    public partial class Production : Form
    {
        private Inventory_DflowEntities db;

        private long _productionId = 0;

        public long ProductionId { get => _productionId; set => _productionId = value; }

        public Production()
        {
            InitializeComponent();

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
                            DgvItems["unit", e.RowIndex].Value = item.unitCode;
                        }
                        else
                        {
                            DgvItems["itemId", e.RowIndex].Value = DBNull.Value;
                            DgvItems["unit", e.RowIndex].Value = DBNull.Value;
                        }
                    }
                }
                
                Calculate_Total_Amount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Calculate_Total_Amount()
        {
            decimal _amount = 0, _totalAmount = 0;

            foreach (DataGridViewRow row in DgvItems.Rows)
            {
                decimal.TryParse(Convert.ToString(value: row.Cells["quantity"].Value), result: out _amount);

                _totalAmount += _amount;
            }

            lblTotalAmount.Text = $"{_totalAmount:0.00}";
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

                                int.TryParse(Convert.ToString(DgvItems["productionId", rowIndex].Value), out int _productionId);

                                DgvItems.Rows.RemoveAt(rowIndex);

                                if (db.productionHeaders.FirstOrDefault(m => m.productionId == _productionId) != null)
                                {
                                    db.productionDetails.Remove(db.productionDetails.FirstOrDefault(m => m.productionId == _productionId));

                                    db.SaveChanges();

                                    var totals = db.productionDetails
                                                .Where(m => m.productionId == db.productionDetails.FirstOrDefault(p => p.productionId == _productionId).productionId)
                                                .GroupBy(m => m.productionId)
                                                .Select(m => new
                                                {
                                                    totalQty = m.Sum(x => x.quantity)
                                                }).SingleOrDefault();

                                    if (db.productionDetails.FirstOrDefault(m => m.productionId == _productionId) != null)
                                    {
                                        if (totals != null)
                                        {
                                            var ph = db.productionHeaders.FirstOrDefault(x => x.productionId == db.productionDetails
                                                                                                                .FirstOrDefault(m => m.productionId == _productionId)
                                                                                                                .productionId);

                                            if (ph != null)
                                            {
                                                ph.totalQuantity = totals.totalQty;
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
                            if (Convert.ToString(row.Cells["itemName"].Value) == Convert.ToString(e.FormattedValue) && row.Index != e.RowIndex)
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
                if (!string.IsNullOrEmpty(Convert.ToString(DgvItems["itemName", e.RowIndex].Value)))
                {
                    decimal.TryParse(Convert.ToString(value: DgvItems["quantity", e.RowIndex].Value), result: out decimal quantity);

                    if (quantity <= 0)
                    {
                        MessageBox.Show("Quantity is empty for Item :" + DgvItems["itemName", e.RowIndex].Value, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
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

                DgvList.DataSource = (from m in db.productionHeaders
                                      select new
                                      {
                                          productionId = m.productionId,
                                          productionDate = m.productionDate,
                                          batchNo = m.batchNo,
                                          totalQuantity = m.totalQuantity,
                                          remark = m.remark
                                      }).ToList();

                Set_Column_Production();
            }
        }

        private void Clear_Controls()
        {
            ProductionId = 0;
            txtBatchNumber.Text = string.Empty;
            txtRemark.Text = string.Empty;

            DgvItems.Rows.Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgvItems.Rows.Count - 1 <= 0)
                {
                    MessageBox.Show("No Items found in grid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            productionHeader ph = new productionHeader();

                            if (ProductionId == 0)
                            {
                                db.productionHeaders.Add(ph);

                                ph.entryBy = SessionHelper.UserId;
                                ph.entryDate = DateTime.Now;
                            }
                            else
                            {
                                ph = db.productionHeaders.FirstOrDefault(m => m.productionId == ProductionId);

                                ph.updatedBy = SessionHelper.UserId;
                                ph.updatedDate = DateTime.Now;
                            }

                            ph.productionDate = (DateTime)CommanMethods.ConvertDate(dtpDate.Text);
                            ph.batchNo = string.IsNullOrWhiteSpace(txtBatchNumber.Text) ? null : txtBatchNumber.Text;
                            ph.remark = string.IsNullOrWhiteSpace(txtRemark.Text) ? null : txtRemark.Text;
                            ph.finYear = string.IsNullOrEmpty(SessionHelper.FinYear) ? null : SessionHelper.FinYear;

                            db.SaveChanges();

                            long _id = ph.productionId;

                            if (_id > 0)
                            {
                                productionDetail pd = new productionDetail();

                                foreach (DataGridViewRow row in DgvItems.Rows)
                                {
                                    pd = new productionDetail();

                                    int.TryParse(Convert.ToString(row.Cells["itemId"].Value), out int _itemId);

                                    if (_itemId > 0)
                                    {
                                        string cmdType = string.Empty;

                                        long.TryParse(Convert.ToString(row.Cells["productionDetailId"].Value), out long _productionDetailId);

                                        if (_productionDetailId == 0)
                                        {
                                            db.productionDetails.Add(pd);
                                            cmdType = "I";
                                        }
                                        else
                                        {
                                            pd = db.productionDetails.FirstOrDefault(x => x.productionDetailId == _productionDetailId);
                                            cmdType = "U";
                                        }

                                        decimal.TryParse(Convert.ToString(row.Cells["quantity"].Value), out decimal _quantity);

                                        pd.productionId = _id;
                                        pd.itemId = _itemId;
                                        pd.quantity = _quantity;

                                        db.sp_Stock_InsertUpdate(CommanMethods.ConvertDate(dtpDate.Text),
                                                                    _itemId, "P", _quantity == 0 ? null : (decimal?)_quantity, null, null, _id, cmdType,
                                                                    SessionHelper.UserId);
                                    }
                                }

                                db.SaveChanges();

                                var totals = db.productionDetails
                                        .Where(m => m.productionId == _id)
                                        .GroupBy(m => m.productionId)
                                        .Select(m => new
                                        {
                                            totalQty = m.Sum(x => x.quantity)
                                        }).SingleOrDefault();

                                if (totals != null)
                                {
                                    ph = db.productionHeaders.FirstOrDefault(x => x.productionId == _id);

                                    if (ph != null)
                                    {
                                        ph.totalQuantity = totals.totalQty;
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
                            return;
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

        private void Set_Column_Production()
        {
            DgvList.Columns["productionId"].Visible = false;

            DgvList.Columns["productionDate"].HeaderText = "Date";
            DgvList.Columns["productionDate"].DisplayIndex = 0;

            DgvList.Columns["batchNo"].HeaderText = "Batch No.";
            DgvList.Columns["batchNo"].DisplayIndex = 1;

            DgvList.Columns["totalQuantity"].HeaderText = "Total Quantity";
            DgvList.Columns["totalQuantity"].DisplayIndex = 2;

            DgvList.Columns["remark"].HeaderText = "Remarks";
            DgvList.Columns["remark"].DisplayIndex = 3;
        }

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                Clear_Controls();

                using (db = new Inventory_DflowEntities())
                {
                    long.TryParse(s: Convert.ToString(DgvList["productionId", RowIndex].Value), result: out long _id);

                    var ph = (from m in db.productionHeaders
                              where m.productionId == _id
                              select new
                              {
                                  productionId = m.productionId,
                                  productionDate = m.productionDate,
                                  batchNumber = m.batchNo,
                                  totalQuantity = m.totalQuantity,
                                  remark = m.remark
                              }).SingleOrDefault();

                    var pd = (from m in db.productionDetails
                              join i in db.ItemMasters on m.itemId equals i.itemId into item
                              from i in item.DefaultIfEmpty()
                              where m.productionId == _id
                              select new
                              {
                                  productionDetailId = m.productionDetailId,
                                  productionId = m.productionId,
                                  itemId = m.itemId,
                                  itemName = i.itemName,
                                  units = i.UnitMaster == null ? "" : i.UnitMaster.unitCode,
                                  quantity = m.quantity
                              }).ToList();

                    if (ph != null)
                    {
                        ProductionId = ph.productionId;
                        dtpDate.Text = ph.productionDate.ToString("dd/MM/yyyy");
                        txtBatchNumber.Text = ph.batchNumber;
                        txtRemark.Text = ph.remark;

                        if (pd != null)
                        {
                            foreach (var item in pd)
                            {
                                DgvItems.Rows.Add(item.itemName, item.units, item.quantity, item.itemId, item.productionDetailId);
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


                        long.TryParse(Convert.ToString(DgvList["productionId", DgvList.CurrentCell.RowIndex].Value), out long _id);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                using (db = new Inventory_DflowEntities())
                                {
                                    if (db.productionHeaders.Where(m => m.productionId == _id).FirstOrDefault() != null)
                                    {
                                        db.productionDetails.Where(m => m.productionId == _id).ToList().ForEach(p => db.productionDetails.Remove(p));

                                        db.productionHeaders.Remove(db.productionHeaders.Where(m => m.productionId == _id).FirstOrDefault());
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
    }
}
