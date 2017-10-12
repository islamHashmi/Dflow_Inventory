using Dflow_Inventory.DataContext;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class UnitMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _unitId = 0;

        public UnitMaster()
        {
            InitializeComponent();

            Get_Data();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtUnitName.Text == string.Empty)
                {
                    MessageBox.Show("Unit Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Unit_Master unit = new Unit_Master();

                    if (_unitId == 0)
                    {
                        db = new Inventory_DflowEntities();

                        db.Unit_Master.Add(unit);

                        unit.entryBy = SessionHelper.UserId;
                        unit.entryDate = DateTime.Now;
                        unit.active = true;
                    }
                    else
                    {
                        unit = db.Unit_Master.FirstOrDefault(m => m.unitId == _unitId && m.active == true);

                        unit.updatedBy = SessionHelper.UserId;
                        unit.updatedDate = DateTime.Now;
                    }

                    unit.unitCode = TxtUnitCode.Text.Trim();
                    unit.unitName = TxtUnitName.Text.Trim();

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

        private void TxtUnitName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                db = new Inventory_DflowEntities();

                var unit = db.Unit_Master.FirstOrDefault(x => x.unitName.ToLower() == TxtUnitName.Text.Trim().ToLower());

                if (unit != null)
                {
                    e.Cancel = true;
                    MessageBox.Show("Unit Name already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw;
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

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int unitId = 0;

                int.TryParse(Convert.ToString(DgvList["unitId", rowIndex]), out unitId);

                using (db = new Inventory_DflowEntities())
                {
                    var unit = db.Unit_Master.FirstOrDefault(x => x.unitId == unitId);

                    if (unit != null)
                    {
                        _unitId = unit.unitId;
                        TxtUnitCode.Text = unit.unitCode;
                        TxtUnitName.Text = unit.unitName;
                    }
                }
            }
        }

        private void Clear_Controls()
        {
            TxtUnitName.Text = string.Empty;
            TxtUnitCode.Text = string.Empty;
            _unitId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                var units = db.Unit_Master
                        .Select(m =>
                        new
                        {
                            unitId = m.unitId,
                            unitName = m.unitName,
                            unitCode = m.unitCode,
                            active = m.active
                        })
                        .Where(m => m.active == true)
                        .ToList();

                DgvList.DataSource = units;

                Set_Column_Unit();
            }
        }

        private void Set_Column_Unit()
        {
            DgvList.Columns["unitId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["unitName"].HeaderText = "Unit Name";
            DgvList.Columns["unitName"].DisplayIndex = 0;

            DgvList.Columns["unitCode"].HeaderText = "Unit Code";
            DgvList.Columns["unitCode"].DisplayIndex = 1;
        }

        private void Link_shorcut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Shortcuts frm = new Shortcuts();
            frm.ShowDialog();
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
                throw;
            }
        }

        private void DgvList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DgvList.CurrentCell != null)
                {
                    if(e.KeyCode == Keys.Enter)
                    {
                        CellContentClick(DgvList.CurrentCell.ColumnIndex, DgvList.CurrentCell.RowIndex);
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        int unitId = 0;

                        int.TryParse(Convert.ToString(DgvList["unitId", DgvList.CurrentCell.RowIndex]), out unitId);

                        using (db = new Inventory_DflowEntities())
                        {
                            var unit = db.Unit_Master.FirstOrDefault(x => x.unitId == unitId);

                            if (unit != null)
                            {
                                unit.active = false;

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
