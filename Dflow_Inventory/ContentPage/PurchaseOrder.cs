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

        private int _purchaseId = 0;

        public PurchaseOrder()
        {
            InitializeComponent();

            Autogenerate_PoNumber();
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
                if (e.ColumnIndex == 0)
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

        private void TxtVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    var Vendors = db.Vendor_Master
                                    .Select(m => new
                                    {
                                        vendorId = m.vendorId,
                                        vendorName = m.vendorName,
                                        active = m.active
                                    })
                                    .Where(m => m.active == true && m.vendorName.Contains(TxtVendorName.Text.Trim()))
                                    .ToList();

                    if (Vendors != null)
                    {
                        LstVendor.DataSource = Vendors;
                        LstVendor.DisplayMember = "vendorName";
                        LstVendor.ValueMember = "vendorId";
                        LstVendor.SelectedIndex = -1;
                    }
                }

                LstVendor_Show();
            }
            catch (Exception ex)
            {
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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

                        var vendor = db.Vendor_Master.FirstOrDefault(x => x.vendorId == _vendorId);

                        if (vendor == null)
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
                throw;
            }
        }

        private void Autogenerate_PoNumber()
        {
            using (db = new Inventory_DflowEntities())
            {
                var poNumber = db.PurchaseHeaders.Max(m => m.poNumber);

                int _poNumber = 0;

                int.TryParse(poNumber, out _poNumber);

                TxtPONumber.Text = string.Format("{0}", _poNumber + 1);
            }
        }

        private void Get_Data()
        {
            throw new NotImplementedException();
        }

        private void Clear_Controls()
        {
            _purchaseId = 0;

            Autogenerate_PoNumber();

            TxtVendorName.Text = string.Empty;
            TxtOrderNo.Text = string.Empty;

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
                    PurchaseHeader ph = new PurchaseHeader();

                    if (_purchaseId == 0)
                    {
                        db.PurchaseHeaders.Add(ph);

                        ph.poNumber = TxtPONumber.Text.Trim();
                        ph.entryBy = SessionHelper.UserId;
                        ph.entryDate = DateTime.Now;
                    }
                    else
                    {
                        ph = db.PurchaseHeaders.FirstOrDefault(m => m.purchaseId == _purchaseId);

                        ph.updatedBy = SessionHelper.UserId;
                        ph.updatedDate = DateTime.Now;
                    }

                    ph.purchaseDate = (DateTime)CommanMethods.ConvertDate(dtpDate.Text);
                    ph.orderNumber = TxtOrderNo.Text.Trim();
                    ph.orderDate = CommanMethods.ConvertDate(dtpOrderDate.Text);

                    int _vendorId = 0;
                    int.TryParse(lblVendorId.Text, out _vendorId);

                    //ph.s




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
    }
}
