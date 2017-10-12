using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class DistributorMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _distributorId = 0;

        public DistributorMaster()
        {
            InitializeComponent();

            Autogenerate_DistributorCode();
            Get_Data();
        }

        private void Autogenerate_DistributorCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                var distributorCode = db.Distributor_Master.Max(m => m.distributorCode);

                int _distributorCode = 0;

                int.TryParse(distributorCode, out _distributorCode);

                TxtDistributorCode.Text = string.Format("{0}", _distributorCode + 1);
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
            _distributorId = 0;

            Autogenerate_DistributorCode();

            TxtDistributorName.Text = string.Empty;
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
                if (TxtDistributorName.Text == string.Empty)
                {
                    MessageBox.Show("Distributor Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Distributor_Master distributor = new Distributor_Master();

                    if (_distributorId == 0)
                    {
                        db.Distributor_Master.Add(distributor);

                        distributor.distributorCode = TxtDistributorCode.Text.Trim();
                        distributor.entryBy = SessionHelper.UserId;
                        distributor.entryDate = DateTime.Now;
                        distributor.active = true;
                    }
                    else
                    {
                        distributor = db.Distributor_Master.FirstOrDefault(m => m.distributorId == _distributorId && m.active == true);

                        distributor.updatedBy = SessionHelper.UserId;
                        distributor.updatedDate = DateTime.Now;
                    }

                    distributor.distributorName = TxtDistributorName.Text.Trim();
                    distributor.address = TxtAddress.Text.Trim();
                    distributor.city = TxtCity.Text.Trim();
                    distributor.state = TxtState.Text.Trim();
                    distributor.pincode = TxtPincode.Text.Trim();
                    distributor.officeNo = TxtOfficeNo.Text.Trim();
                    distributor.mobileNo = TxtMobile.Text.Trim();
                    distributor.faxNo = TxtFaxNo.Text.Trim();
                    distributor.emailId = TxtEmailId.Text.Trim();
                    distributor.gstNo = TxtGstNo.Text.Trim();
                    distributor.panNo = TxtPanNo.Text.Trim();

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
                int distributorId = 0;

                int.TryParse(Convert.ToString(DgvList["distributorId", rowIndex].Value), out distributorId);

                using (db = new Inventory_DflowEntities())
                {
                    var sm = db.Distributor_Master.FirstOrDefault(x => x.distributorId == distributorId);

                    if (sm != null)
                    {
                        _distributorId = sm.distributorId;
                        TxtDistributorCode.Text = sm.distributorCode;
                        TxtDistributorName.Text = sm.distributorName;
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
                        int distributorId = 0;

                        int.TryParse(Convert.ToString(DgvList["supplierId", DgvList.CurrentCell.RowIndex]), out distributorId);

                        using (db = new Inventory_DflowEntities())
                        {
                            var distributor = db.Distributor_Master.FirstOrDefault(x => x.distributorId == distributorId);

                            if (distributor != null)
                            {
                                distributor.active = false;

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
                var suppliers = db.Distributor_Master
                                .Select(m => new
                                {
                                    distributorId = m.distributorId,
                                    distributorCode = m.distributorCode,
                                    distributorName = m.distributorName,
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

                Set_Column_Distributor();
            }
        }

        private void Set_Column_Distributor()
        {
            DgvList.Columns["distributorId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["distributorCode"].HeaderText = "Distributor Code";
            DgvList.Columns["distributorCode"].DisplayIndex = 0;

            DgvList.Columns["distributorName"].HeaderText = "Name";
            DgvList.Columns["distributorName"].DisplayIndex = 1;

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
