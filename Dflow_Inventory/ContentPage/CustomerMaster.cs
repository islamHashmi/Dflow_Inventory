﻿using Dflow_Inventory.DataContext;
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
    public partial class CustomerMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _customerId = 0;

        public int CustomerId { get => _customerId; set => _customerId = value; }

        public CustomerMaster()
        {
            InitializeComponent();

            ComboBox();
            Autogenerate_CustomerCode();
            Get_Data();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                cmbType.DataSource = db.customerTypes.ToList();
                cmbType.ValueMember = "typeCode";
                cmbType.DisplayMember = "typeDescription";
            }
        }

        private void Autogenerate_CustomerCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                int _customerCode = 0;

                int.TryParse(db.CustomerMasters.Max(m => m.customerCode), out _customerCode);

                TxtCustomerCode.Text = string.Format("{0}", _customerCode + 1);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear_Controls()
        {
            CustomerId = 0;

            Autogenerate_CustomerCode();

            TxtCustomerName.Text = string.Empty;
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
                if (TxtCustomerName.Text == string.Empty)
                {
                    MessageBox.Show("Customer Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    DataContext.CustomerMaster customer = new DataContext.CustomerMaster();

                    if (CustomerId == 0)
                    {
                        db.CustomerMasters.Add(customer);

                        customer.customerCode = TxtCustomerCode.Text.Trim();
                        customer.entryBy = SessionHelper.UserId;
                        customer.entryDate = DateTime.Now;
                        customer.active = true;
                    }
                    else
                    {
                        customer = db.CustomerMasters.FirstOrDefault(m => m.customerId == CustomerId && m.active == true);

                        customer.updatedBy = SessionHelper.UserId;
                        customer.updatedDate = DateTime.Now;
                    }

                    customer.registerType = Convert.ToString(cmbType.SelectedValue);
                    customer.customerName = TxtCustomerName.Text.Trim();
                    customer.address = TxtAddress.Text.Trim();
                    customer.city = TxtCity.Text.Trim();
                    customer.state = TxtState.Text.Trim();
                    customer.pincode = TxtPincode.Text.Trim();
                    customer.officeNo = TxtOfficeNo.Text.Trim();
                    customer.mobileNo = TxtMobile.Text.Trim();
                    customer.faxNo = TxtFaxNo.Text.Trim();
                    customer.emailId = TxtEmailId.Text.Trim();
                    customer.gstNo = TxtGstNo.Text.Trim();
                    customer.panNo = TxtPanNo.Text.Trim();

                    db.SaveChanges();

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

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int customerId = 0;

                int.TryParse(Convert.ToString(DgvList["customerId", rowIndex].Value), out customerId);

                using (db = new Inventory_DflowEntities())
                {
                    var sm = db.CustomerMasters.FirstOrDefault(x => x.customerId == customerId);

                    if (sm != null)
                    {
                        CustomerId = sm.customerId;
                        TxtCustomerCode.Text = sm.customerCode;
                        TxtCustomerName.Text = sm.customerName;
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
                        int.TryParse(Convert.ToString(value: DgvList["supplierId", DgvList.CurrentCell.RowIndex]), result: out int customerId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.CustomerMasters.FirstOrDefault(x => x.customerId == customerId) != null)
                            {
                                db.CustomerMasters.FirstOrDefault(x => x.customerId == customerId).active = false;

                                db.SaveChanges();
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

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = db.CustomerMasters
                                .Select(m => new
                                {
                                    customerId = m.customerId,
                                    customerCode = m.customerCode,
                                    customerName = m.customerName,
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
            DgvList.Columns["customerId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["customerCode"].HeaderText = "Customer Code";
            DgvList.Columns["customerCode"].DisplayIndex = 0;

            DgvList.Columns["customerName"].HeaderText = "Name";
            DgvList.Columns["customerName"].DisplayIndex = 1;

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
        
        private void cmbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string text = cmbType.GetItemText(cmbType.SelectedItem);

                lblRegCode.Text = text + " Code";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
