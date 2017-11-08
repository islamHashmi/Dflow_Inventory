using System;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using System.Linq;
using Dflow_Inventory.Helpers;
using System.Drawing;
using System.Transactions;

namespace Dflow_Inventory.ContentPage
{
    public partial class ItemMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _itemId = 0;

        public int ItemId { get => _itemId; set => _itemId = value; }

        public ItemMaster()
        {
            InitializeComponent();

            TxtItemName.Focus();

            Autogenerate_ItemCode();

            ComboBox();

            Get_Data();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtItemName.Text == string.Empty)
                {
                    MessageBox.Show("Item Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        using (db = new Inventory_DflowEntities())
                        {
                            DataContext.ItemMaster item = new DataContext.ItemMaster();

                            decimal.TryParse(TxtPrice.Text, out decimal _sellingPrice);
                            decimal.TryParse(TxtOpeningStk.Text, out decimal _openStock);

                            if (ItemId == 0)
                            {
                                db.ItemMasters.Add(item);

                                item.itemCode = TxtItemCode.Text.Trim();
                                item.entryBy = SessionHelper.UserId;
                                item.entryDate = DateTime.Now;
                                item.active = true;
                                item.currentStock = _openStock == 0 ? null : (decimal?)_openStock;
                            }
                            else
                            {
                                item = db.ItemMasters.FirstOrDefault(m => m.itemId == ItemId && m.active == true);

                                item.updatedBy = SessionHelper.UserId;
                                item.updatedDate = DateTime.Now;
                            }

                            item.itemName = TxtItemName.Text;
                            item.itemDescription = string.IsNullOrWhiteSpace(TxtDescription.Text) ? null : TxtDescription.Text;
                            item.unitId = Convert.ToString(CmbUnit.SelectedValue) == "0" ? null : (int?)Convert.ToInt32(CmbUnit.SelectedValue);
                            item.sellingPrice = _sellingPrice == 0 ? null : (decimal?)_sellingPrice;
                            item.openingStock = _openStock == 0 ? null : (decimal?)_openStock;
                            item.rawMaterial = chkRawMaterial.Checked;

                            db.SaveChanges();

                            int _id = item.itemId;

                            if (_openStock != 0)
                            {
                                Stock stock = new Stock();

                                var stk = db.Stocks
                                            .Where(m => m.itemId == ItemId && m.stockType == "O")
                                            .OrderByDescending(m => m.stockId)
                                            .FirstOrDefault();

                                if (stk == null)
                                {
                                    stock.stockDate = DateTime.Today;
                                    stock.invoiceId = null;
                                    stock.purchaseId = null;
                                    stock.itemId = _id;
                                    stock.stockType = "O";
                                    stock.openingStock = _openStock == 0 ? null : (decimal?)_openStock;
                                    stock.quantity = null;
                                    stock.closingStock = _openStock == 0 ? null : (decimal?)_openStock;
                                    stock.remark = "Opening Stock : " + Convert.ToString(_openStock);
                                    stock.entryBy = SessionHelper.UserId;
                                    stock.entryDate = DateTime.Now;

                                    db.Stocks.Add(stock);

                                    db.SaveChanges();
                                }
                            }
                            scope.Complete();
                        }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear_Controls()
        {
            TxtItemName.Text = string.Empty;
            TxtDescription.Text = string.Empty;
            TxtOpeningStk.Text = string.Empty;
            TxtPrice.Text = string.Empty;
            CmbUnit.SelectedValue = 0;

            TxtOpeningStk.ReadOnly = false;

            ItemId = 0;

            Autogenerate_ItemCode();
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

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var units = db.UnitMasters
                            .Select(m => new
                            {
                                unitName = m.unitName + " (" + m.unitCode + ")",
                                unitId = m.unitId,
                                active = m.active
                            })
                            .Where(m => m.active == true)
                            .ToList();

                units.Insert(0, new { unitName = "--- Select Unit ---", unitId = 0, active = true });

                CmbUnit.DataSource = units;
                CmbUnit.DisplayMember = "unitName";
                CmbUnit.ValueMember = "unitId";
            }
        }

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int.TryParse(Convert.ToString(DgvList["itemId", rowIndex].Value), out int itemId);

                using (db = new Inventory_DflowEntities())
                {
                    var item = db.ItemMasters.FirstOrDefault(x => x.itemId == itemId);

                    if (item != null)
                    {
                        ItemId = item.itemId;
                        TxtItemCode.Text = item.itemCode;
                        TxtItemName.Text = item.itemName;
                        TxtDescription.Text = item.itemDescription;
                        TxtPrice.Text = Convert.ToString(item.sellingPrice);
                        TxtOpeningStk.Text = Convert.ToString(item.openingStock);
                        CmbUnit.SelectedValue = item.unitId ?? 0;
                        chkRawMaterial.Checked = item.rawMaterial;

                        TxtOpeningStk.ReadOnly = true;
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

        private void Autogenerate_ItemCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(db.ItemMasters.Max(m => m.itemCode), out int _itemCode);

                TxtItemCode.Text = $"{_itemCode + 1}";
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from m in db.ItemMasters
                                      join b in db.UnitMasters on m.unitId equals b.unitId into unit
                                      from b in unit.DefaultIfEmpty()
                                      where m.active == true
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
                                      }).ToList();

                Set_Column_Unit();
            }
        }

        private void Set_Column_Unit()
        {
            DgvList.Columns["itemId"].Visible = false;
            DgvList.Columns["unitId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["itemCode"].HeaderText = "Item Code";
            DgvList.Columns["itemCode"].DisplayIndex = 0;

            DgvList.Columns["itemName"].HeaderText = "Item Name";
            DgvList.Columns["itemName"].DisplayIndex = 1;

            DgvList.Columns["description"].HeaderText = "Description";
            DgvList.Columns["description"].DisplayIndex = 3;

            DgvList.Columns["unitCode"].HeaderText = "Unit Code";
            DgvList.Columns["unitCode"].DisplayIndex = 2;

            DgvList.Columns["sellingPrice"].HeaderText = "Selling Price";
            DgvList.Columns["sellingPrice"].DisplayIndex = 4;

            DgvList.Columns["openingStock"].HeaderText = "Opening Stock";
            DgvList.Columns["openingStock"].DisplayIndex = 5;
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


                        int.TryParse(s: Convert.ToString(DgvList["itemId", DgvList.CurrentCell.RowIndex].Value), result: out int itemId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.ItemMasters.FirstOrDefault(x => x.itemId == itemId) != null)
                            {
                                db.ItemMasters.FirstOrDefault(x => x.itemId == itemId).active = false;

                                db.SaveChanges();

                                Get_Data();
                            }
                            else
                            {
                                MessageBox.Show("Cannot delete this item.");
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

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtOpeningStk_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
