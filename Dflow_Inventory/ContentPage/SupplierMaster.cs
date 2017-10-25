using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class SupplierMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _supplierId = 0;

        public int SupplierId { get => _supplierId; set => _supplierId = value; }

        public SupplierMaster()
        {
            InitializeComponent();

            Autogenerate_SupplierCode();

            Get_Data();
        }

        private void Autogenerate_SupplierCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(db.Supplier_Master.Max(m => m.supplierCode), out int _supplierCode);

                TxtSupplierCode.Text = $"{_supplierCode + 1}";
            }
        }

        private void TxtEmailId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtEmailId.Text))
                {
                    if (!(bool)CommanMethods.Validate_Email(TxtEmailId.Text.Trim()))
                    {
                        MessageBox.Show("Invalid Email Id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Clear_Controls()
        {
            SupplierId = 0;

            Autogenerate_SupplierCode();

            TxtSupplierName.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtCity.Text = string.Empty;
            TxtState.Text = string.Empty;
            TxtPincode.Text = string.Empty;
            TxtMobile.Text = string.Empty;
            TxtOfficeNo.Text = string.Empty;
            TxtFaxNo.Text = string.Empty;
            TxtEmailId.Text = string.Empty;
            TxtGstNo.Text = string.Empty;
            TxtPanNo.Text = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtSupplierName.Text == string.Empty)
                {
                    MessageBox.Show("Supplier Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Supplier_Master supplier = new Supplier_Master();

                    if (SupplierId == 0)
                    {
                        db.Supplier_Master.Add(supplier);

                        supplier.supplierCode = TxtSupplierCode.Text.Trim();
                        supplier.entryBy = SessionHelper.UserId;
                        supplier.entryDate = DateTime.Now;
                        supplier.active = true;
                    }
                    else
                    {
                        supplier = db.Supplier_Master.FirstOrDefault(m => m.supplierId == SupplierId && m.active == true);

                        supplier.updatedBy = SessionHelper.UserId;
                        supplier.updatedDate = DateTime.Now;
                    }

                    supplier.supplierName = TxtSupplierName.Text.Trim();
                    supplier.address = TxtAddress.Text.Trim();
                    supplier.city = TxtCity.Text.Trim();
                    supplier.state = TxtState.Text.Trim();
                    supplier.pincode = TxtPincode.Text.Trim();
                    supplier.officeNo = TxtOfficeNo.Text.Trim();
                    supplier.mobileNo = TxtMobile.Text.Trim();
                    supplier.faxNo = TxtFaxNo.Text.Trim();
                    supplier.emailId = TxtEmailId.Text.Trim();
                    supplier.gstNo = TxtGstNo.Text.Trim();
                    supplier.panNo = TxtPanNo.Text.Trim();

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

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int.TryParse(s: Convert.ToString(DgvList["supplierId", rowIndex].Value), result: out int supplierId);

                using (db = new Inventory_DflowEntities())
                {
                    var sm = db.Supplier_Master.FirstOrDefault(x => x.supplierId == supplierId);

                    if (sm != null)
                    {
                        SupplierId = sm.supplierId;
                        TxtSupplierCode.Text = sm.supplierCode;
                        TxtSupplierName.Text = sm.supplierName;
                        TxtAddress.Text = sm.address;
                        TxtCity.Text = sm.city;
                        TxtState.Text = sm.state;
                        TxtPincode.Text = sm.pincode;
                        TxtMobile.Text = sm.mobileNo;
                        TxtOfficeNo.Text = sm.officeNo;
                        TxtFaxNo.Text = sm.faxNo;
                        TxtEmailId.Text = sm.emailId;
                        TxtGstNo.Text = sm.gstNo;
                        TxtPanNo.Text = sm.panNo;
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
                        int.TryParse(s: Convert.ToString(DgvList["supplierId", DgvList.CurrentCell.RowIndex]), result: out int supplierId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.Supplier_Master.FirstOrDefault(x => x.supplierId == supplierId) != null)
                            {
                                db.Supplier_Master.FirstOrDefault(x => x.supplierId == supplierId).active = false;

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

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = db.Supplier_Master
                                .Select(m => new
                                {
                                    supplierId = m.supplierId,
                                    supplierCode = m.supplierCode,
                                    supplierName = m.supplierName,
                                    address = m.address,
                                    city = m.city,
                                    state = m.state,
                                    pincode = m.pincode,
                                    officeNo = m.officeNo,
                                    mobileNo = m.mobileNo,
                                    faxNo = m.faxNo,
                                    emailId = m.emailId,
                                    gstNo = m.gstNo,
                                    panNo = m.panNo,
                                    active = m.active
                                }).Where(m => m.active == true).ToList();

                Set_Column_Unit();
            }
        }

        private void Set_Column_Unit()
        {
            DgvList.Columns["supplierId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["supplierCode"].HeaderText = "Supplier Code";
            DgvList.Columns["supplierCode"].DisplayIndex = 0;

            DgvList.Columns["supplierName"].HeaderText = "Name";
            DgvList.Columns["supplierName"].DisplayIndex = 1;

            DgvList.Columns["address"].HeaderText = "Address";
            DgvList.Columns["address"].DisplayIndex = 2;

            DgvList.Columns["city"].HeaderText = "City";
            DgvList.Columns["city"].DisplayIndex = 3;

            DgvList.Columns["state"].HeaderText = "State";
            DgvList.Columns["state"].DisplayIndex = 4;

            DgvList.Columns["pincode"].HeaderText = "Pincode";
            DgvList.Columns["pincode"].DisplayIndex = 5;

            DgvList.Columns["officeNo"].HeaderText = "Office No.";
            DgvList.Columns["officeNo"].DisplayIndex = 6;

            DgvList.Columns["mobileNo"].HeaderText = "Mobile No.";
            DgvList.Columns["mobileNo"].DisplayIndex = 7;

            DgvList.Columns["faxNo"].HeaderText = "Fax No.";
            DgvList.Columns["faxNo"].DisplayIndex = 8;

            DgvList.Columns["emailId"].HeaderText = "Email Id";
            DgvList.Columns["emailId"].DisplayIndex = 9;

            DgvList.Columns["gstNo"].HeaderText = "GST No.";
            DgvList.Columns["gstNo"].DisplayIndex = 10;

            DgvList.Columns["panNo"].HeaderText = "PAN No.";
            DgvList.Columns["panNo"].DisplayIndex = 11;
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
    }
}
