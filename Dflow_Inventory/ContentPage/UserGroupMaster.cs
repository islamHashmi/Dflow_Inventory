using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class UserGroupMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _userGroupId = 0;

        public UserGroupMaster()
        {
            InitializeComponent();

            Get_Data();
        }

        private void TxtGroupName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtGroupName.Text))
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        var grp = db.User_Group.FirstOrDefault(m => m.groupName == TxtGroupName.Text.Trim());

                        if (grp != null)
                        {
                            MessageBox.Show("Group Name already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtGroupName.Text))
                {
                    MessageBox.Show("Group Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TxtGroupName.Focus();
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    User_Group grp = new User_Group();

                    if (_userGroupId == 0)
                    {
                        db.User_Group.Add(grp);
                        grp.entryBy = SessionHelper.UserGroupId;
                        grp.entryDate = DateTime.Now;
                        grp.active = true;
                    }
                    else
                    {
                        grp = db.User_Group.FirstOrDefault(m => m.userGroupId == _userGroupId);
                        grp.updatedBy = SessionHelper.UserId;
                        grp.updatedOn = DateTime.Now;
                    }

                    grp.groupName = TxtGroupName.Text.Trim();

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
            TxtGroupName.Text = string.Empty;

            _userGroupId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                var grps = db.User_Group
                           .Select(m => new
                           {
                               userGroupId = m.userGroupId,
                               groupName = m.groupName,
                               active = m.active
                           }).Where(m => m.active == true)
                           .ToList();

                DgvList.DataSource = grps;

                Set_Column();
            }
        }

        private void Set_Column()
        {
            DgvList.Columns["userGroupId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["groupName"].HeaderText = "Group Name";
            DgvList.Columns["groupName"].DisplayIndex = 0;
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

                int _id = 0;

                int.TryParse(Convert.ToString(DgvList["userGroupId", RowIndex].Value), out _id);

                if (_id != 0)
                {
                    using (db = new Inventory_DflowEntities())
                    {
                        var ug = db.User_Group.FirstOrDefault(m => m.userGroupId == _id);

                        if (ug != null)
                        {
                            _userGroupId = ug.userGroupId;
                            TxtGroupName.Text = ug.groupName;
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

                        int _id = 0;

                        int.TryParse(Convert.ToString(DgvList["userGroupId", DgvList.CurrentCell.RowIndex].Value), out _id);

                        using (db = new Inventory_DflowEntities())
                        {
                            var ug = db.User_Group.FirstOrDefault(m => m.userGroupId == _id);

                            if (ug != null)
                            {
                                ug.active = false;
                                ug.updatedBy = SessionHelper.UserId;
                                ug.updatedOn = DateTime.Now;

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
    }
}
