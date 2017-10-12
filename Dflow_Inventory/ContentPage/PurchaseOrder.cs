using System.Drawing;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Linq;
using System;

namespace Dflow_Inventory.ContentPage
{
    public partial class PurchaseOrder : Form
    {
        private Inventory_DflowEntities db;

        private int _purchaseOrderId = 0;

        public PurchaseOrder()
        {
            InitializeComponent();
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
                            string name = item.itemCode + " - " + item.itemName;

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
                if(e.ColumnIndex == 0)
                {
                    string itemName = Convert.ToString(DgvItems[e.ColumnIndex, e.RowIndex].Value);

                    string[] split = itemName.Split('-');

                    string itemCode = split[0];

                    using (db = new Inventory_DflowEntities())
                    {
                        var item = (from m in db.Item_Master
                                    join b in db.Unit_Master on m.unitId equals b.unitId into unit
                                    from b in unit.DefaultIfEmpty()
                                    where m.itemCode == itemCode
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

            DgvItems["Col_Amount", RowIndex].Value = _amount;
        }

        private void TxtSupplierName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LstSupplier.Items.Clear();

                using (db = new Inventory_DflowEntities())
                {
                    var suppliers = db.Supplier_Master
                                    .Select(m => new
                                    {
                                        supplierId = m.supplierId,
                                        supplierName = m.supplierName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.supplierName.Contains(TxtSupplierName.Text.Trim()))
                                    .ToList();

                    if(suppliers != null)
                    {
                        foreach(var s in suppliers)
                        {
                            LstSupplier.Items.Add(s.supplierName);
                        }
                    }
                }

                LstSupplier_Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstSupplier_Show()
        {
            LstSupplier.Show();
            LstSupplier.BringToFront();
            LstSupplier.Location = new Point(TxtSupplierName.Location.X, TxtSupplierName.Location.Y + 25);
            LstSupplier.Width = TxtSupplierName.Width;
            LstSupplier.Height = 200;
        }

        private void TxtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Down)
                {
                    LstSupplier_Show();

                    LstSupplier.Focus();
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LstSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    TxtSupplierName.Text = Convert.ToString(LstSupplier.SelectedItem);

                    LstSupplier.Hide();
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Autogenerate_PoNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                var poNumber = db.Item_Master.Max(m => m.itemCode);

                int _poNumber = 0;

                int.TryParse(poNumber, out _poNumber);

                TxtPONumber.Text = string.Format("{0}", _poNumber + 1);
            }
        }
    }
}
