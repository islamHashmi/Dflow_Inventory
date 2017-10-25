using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Drawing;

namespace Dflow_Inventory.ContentPage
{
    public partial class UserMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _userId = 0;

        public int UserId { get => _userId; set => _userId = value; }

        public UserMaster()
        {
            InitializeComponent();

            ComboBox();
            Get_Data();
        }

        private void ComboBox()
        {
            using (db = new Inventory_DflowEntities())
            {
                var groups = db.User_Group
                            .Select(m => new
                            {
                                userGroupId = m.userGroupId,
                                groupName = m.groupName,
                                active = m.active
                            }).Where(m => m.active == true).ToList();

                groups.Insert(0, new { userGroupId = 0, groupName = "--- Select Group Name ---", active = true });

                cmbUserGroup.DataSource = groups;
                cmbUserGroup.DisplayMember = "groupName";
                cmbUserGroup.ValueMember = "userGroupId";
            }
        }

        private void TxtLoginId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtLoginId.Text) && UserId == 0)
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        if (db.User_Master.FirstOrDefault(m => m.loginId == TxtLoginId.Text.Trim()) != null)
                        {
                            MessageBox.Show("Login Id already exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void TxtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtConfirmPass.Text))
                {
                    if (TxtConfirmPass.Text.Trim() != TxtPassword.Text.Trim())
                    {
                        MessageBox.Show(text: "Both passwords do not match.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TxtMobile_KeyPress(object sender, KeyPressEventArgs e)
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
                throw;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtLoginId.Text))
                {
                    MessageBox.Show(text: "Login Id is mandatory.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                    TxtLoginId.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    MessageBox.Show(text: "Password is mandatory.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                    TxtPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    MessageBox.Show(text: "Name is mandatory.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                    TxtName.Focus();
                    return;
                }

                if (cmbUserGroup.SelectedIndex <= 0)
                {
                    MessageBox.Show(text: "Select User Group.", caption: "Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);
                    cmbUserGroup.Focus();
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    User_Master um = new User_Master();

                    if (UserId == 0)
                    {
                        db.User_Master.Add(um);
                        um.entryBy = SessionHelper.UserId;
                        um.entryDate = DateTime.Now;
                        um.active = true;
                    }
                    else
                    {
                        um = db.User_Master.FirstOrDefault(m => m.userId == UserId);
                        um.updatedBy = SessionHelper.UserId;
                        um.updatedOn = DateTime.Now;
                    }

                    um.loginId = string.IsNullOrEmpty(TxtLoginId.Text) ? null : TxtLoginId.Text;
                    um.loginKey = string.IsNullOrEmpty(TxtPassword.Text) ? null : TxtPassword.Text;
                    um.userGroupId = Convert.ToInt32(cmbUserGroup.SelectedValue);
                    um.name = string.IsNullOrEmpty(TxtName.Text) ? null : TxtName.Text;
                    um.mobileNumber = string.IsNullOrEmpty(TxtMobile.Text) ? null : TxtMobile.Text;
                    um.emailId = string.IsNullOrEmpty(TxtEmail.Text) ? null : TxtEmail.Text;

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

        private void Clear_Controls()
        {
            TxtLoginId.Text = string.Empty;
            TxtPassword.Text = string.Empty;
            TxtConfirmPass.Text = string.Empty;
            TxtName.Text = string.Empty;
            cmbUserGroup.SelectedIndex = 0;
            TxtMobile.Text = string.Empty;
            TxtEmail.Text = string.Empty;

            UserId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from a in db.User_Master
                                      join b in db.User_Group on a.userGroupId equals b.userGroupId into grp
                                      from b in grp.DefaultIfEmpty()
                                      where a.active == true
                                      select new
                                      {
                                          userId = a.userId,
                                          loginId = a.loginId,
                                          name = a.name,
                                          mobile = a.mobileNumber,
                                          emailId = a.emailId,
                                          userGroupId = a.userGroupId,
                                          userGroup = b.groupName,
                                          active = a.active
                                      }).ToList();

                Set_Column();
            }
        }

        private void Set_Column()
        {
            DgvList.Columns["userId"].Visible = false;
            DgvList.Columns["active"].Visible = false;
            DgvList.Columns["userGroupId"].Visible = false;

            DgvList.Columns["name"].HeaderText = "Name";
            DgvList.Columns["name"].DisplayIndex = 0;

            DgvList.Columns["loginId"].HeaderText = "Login Id";
            DgvList.Columns["loginId"].DisplayIndex = 1;

            DgvList.Columns["mobile"].HeaderText = "Mobile Number";
            DgvList.Columns["mobile"].DisplayIndex = 2;

            DgvList.Columns["emailId"].HeaderText = "Email Address";
            DgvList.Columns["emailId"].DisplayIndex = 3;

            DgvList.Columns["userGroup"].HeaderText = "User Group";
            DgvList.Columns["userGroup"].DisplayIndex = 4;
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

        private void CellContentClick(int ColumnIndex, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                Clear_Controls();
                
                int.TryParse(Convert.ToString(DgvList["userId", RowIndex].Value), out int _id);

                if (_id != 0)
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        var um = db.User_Master.FirstOrDefault(m => m.userId == _id);

                        if (um != null)
                        {
                            UserId = um.userId;
                            TxtLoginId.Text = um.loginId;
                            TxtPassword.Text = um.loginKey;
                            TxtConfirmPass.Text = um.loginKey;
                            TxtName.Text = um.name;
                            TxtMobile.Text = um.mobileNumber.Trim();
                            TxtEmail.Text = um.emailId;
                            cmbUserGroup.SelectedValue = um.userGroupId;
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
                        if (DialogResult.No == MessageBox.Show("Are you sure to delete this record ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            return;
                        
                        int.TryParse(s: Convert.ToString(DgvList["userId", DgvList.CurrentCell.RowIndex].Value), result: out int _id);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.User_Master.FirstOrDefault(m => m.userId == _id) != null)
                            {
                                db.User_Master.FirstOrDefault(m => m.userId == _id).active = false;
                                db.User_Master.FirstOrDefault(m => m.userId == _id).updatedBy = SessionHelper.UserId;
                                db.User_Master.FirstOrDefault(m => m.userId == _id).updatedOn = DateTime.Now;

                                db.SaveChanges();
                            }
                        }
                    }
                    else if(e.KeyData ==(Keys.Shift | Keys.P))
                    {
                        int.TryParse(s: Convert.ToString(DgvList["userId", DgvList.CurrentCell.RowIndex].Value), result: out int _id);

                        if (_id != 0)
                        {
                            ChangePassword frm = new ChangePassword(_id, Convert.ToString(DgvList["loginId", DgvList.CurrentCell.RowIndex].Value));
                            frm.Show();
                        }
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
