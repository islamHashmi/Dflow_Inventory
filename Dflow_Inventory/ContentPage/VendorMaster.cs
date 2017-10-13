using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class VendorMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _vendorId = 0;

        public VendorMaster()
        {
            InitializeComponent();

            Autogenerate_vendorCode();
            Get_Data();
        }

        private void Autogenerate_vendorCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                var vendorCode = db.Vendor_Master.Max(m => m.vendorCode);

                int _vendorCode = 0;

                int.TryParse(vendorCode, out _vendorCode);

                TxtVendorCode.Text = string.Format("{0}", _vendorCode + 1);
            }
        }

        private void TxtEmailId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtEmailId.Text))
                {
                    bool flag = CommanMethods.Validate_Email(TxtEmailId.Text.Trim());

                    if (!flag)
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
            _vendorId = 0;

            Autogenerate_vendorCode();

            TxtVendorName.Text = string.Empty;
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
                if (TxtVendorName.Text == string.Empty)
                {
                    MessageBox.Show("vendor Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Vendor_Master vendor = new Vendor_Master();

                    if (_vendorId == 0)
                    {
                        db.Vendor_Master.Add(vendor);

                        vendor.vendorCode = TxtVendorCode.Text.Trim();
                        vendor.entryBy = SessionHelper.UserId;
                        vendor.entryDate = DateTime.Now;
                        vendor.active = true;
                    }
                    else
                    {
                        vendor = db.Vendor_Master.FirstOrDefault(m => m.vendorId == _vendorId && m.active == true);

                        vendor.updatedBy = SessionHelper.UserId;
                        vendor.updatedDate = DateTime.Now;
                    }

                    vendor.vendorName = TxtVendorName.Text.Trim();
                    vendor.address = TxtAddress.Text.Trim();
                    vendor.city = TxtCity.Text.Trim();
                    vendor.state = TxtState.Text.Trim();
                    vendor.pincode = TxtPincode.Text.Trim();
                    vendor.officeNo = TxtOfficeNo.Text.Trim();
                    vendor.mobileNo = TxtMobile.Text.Trim();
                    vendor.faxNo = TxtFaxNo.Text.Trim();
                    vendor.emailId = TxtEmailId.Text.Trim();
                    vendor.gstNo = TxtGstNo.Text.Trim();
                    vendor.panNo = TxtPanNo.Text.Trim();

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
                int vendorId = 0;

                int.TryParse(Convert.ToString(DgvList["vendorId", rowIndex].Value), out vendorId);

                using (db = new Inventory_DflowEntities())
                {
                    var sm = db.Vendor_Master.FirstOrDefault(x => x.vendorId == vendorId);

                    if (sm != null)
                    {
                        _vendorId = sm.vendorId;
                        TxtVendorCode.Text = sm.vendorCode;
                        TxtVendorName.Text = sm.vendorName;
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
                        int vendorId = 0;

                        int.TryParse(Convert.ToString(DgvList["supplierId", DgvList.CurrentCell.RowIndex]), out vendorId);

                        using (db = new Inventory_DflowEntities())
                        {
                            var vendor = db.Vendor_Master.FirstOrDefault(x => x.vendorId == vendorId);

                            if (vendor != null)
                            {
                                vendor.active = false;

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
                var suppliers = db.Vendor_Master
                                .Select(m => new
                                {
                                    vendorId = m.vendorId,
                                    vendorCode = m.vendorCode,
                                    vendorName = m.vendorName,
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


                DgvList.DataSource = suppliers;

                Set_Column_Unit();
            }
        }

        private void Set_Column_Unit()
        {
            DgvList.Columns["vendorId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["vendorCode"].HeaderText = "vendor Code";
            DgvList.Columns["vendorCode"].DisplayIndex = 0;

            DgvList.Columns["vendorName"].HeaderText = "Name";
            DgvList.Columns["vendorName"].DisplayIndex = 1;

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
