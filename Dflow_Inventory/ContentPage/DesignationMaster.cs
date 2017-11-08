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
    public partial class DesignationMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _designationId = 0;

        public DesignationMaster()
        {
            InitializeComponent();
            Get_Data();
        }

        private void TxtDesignation_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtDesignation.Text))
            {
                return;
            }

            using (db = new Inventory_DflowEntities())
            {
                if (db.Designations.FirstOrDefault(m => m.designationName == TxtDesignation.Text.Trim()) != null)
                {
                    e.Cancel = true;
                    MessageBox.Show("Designation Name already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtDesignation.Text == string.Empty)
                {
                    MessageBox.Show("Designation Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Designation designation = new Designation();

                    if (_designationId == 0)
                    {
                        db = new Inventory_DflowEntities();

                        db.Designations.Add(designation);

                        designation.entryBy = SessionHelper.UserId;
                        designation.entryDate = DateTime.Now;
                        designation.active = true;
                    }
                    else
                    {
                        designation = db.Designations.FirstOrDefault(m => m.designationId == _designationId && m.active == true);

                        designation.updatedBy = SessionHelper.UserId;
                        designation.updatedDate = DateTime.Now;
                    }

                    designation.designationName = TxtDesignation.Text.Trim();

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

        private void Clear_Controls()
        {
            TxtDesignation.Text = string.Empty;
            _designationId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = db.Designations
                        .Select(m =>
                        new
                        {
                            designationId = m.designationId,
                            designationName = m.designationName,
                            active = m.active
                        })
                        .Where(m => m.active == true)
                        .ToList();

                Set_Column_Unit();
            }
        }

        private void Set_Column_Unit()
        {
            DgvList.Columns["designationId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["designationName"].HeaderText = "Designation Name";
            DgvList.Columns["designationName"].DisplayIndex = 0;
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

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int.TryParse(Convert.ToString(DgvList["designationId", rowIndex].Value), out int designationId);

                using (db = new Inventory_DflowEntities())
                {
                    var designation = db.Designations.FirstOrDefault(x => x.designationId == designationId);

                    if (designation != null)
                    {
                        _designationId = designation.designationId;
                        TxtDesignation.Text = designation.designationName;
                    }
                }
            }
        }

        private void DgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgvList.CurrentCell != null)
                {
                    CellContentClick(DgvList.CurrentCell.ColumnIndex, DgvList.CurrentCell.RowIndex);
                }
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
                        int.TryParse(Convert.ToString(DgvList["designationId", DgvList.CurrentCell.RowIndex].Value), out int designationId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.Designations.FirstOrDefault(x => x.designationId == designationId) != null)
                            {
                                db.Designations.FirstOrDefault(x => x.designationId == designationId).active = false;

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
    }
}
