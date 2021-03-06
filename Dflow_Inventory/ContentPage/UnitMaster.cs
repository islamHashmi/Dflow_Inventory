﻿using Dflow_Inventory.DataContext;
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

        public int UnitId { get => _unitId; set => _unitId = value; }

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
                    DataContext.UnitMaster unit = new DataContext.UnitMaster();

                    if (UnitId == 0)
                    {
                        db = new Inventory_DflowEntities();

                        db.UnitMasters.Add(unit);

                        unit.entryBy = SessionHelper.UserId;
                        unit.entryDate = DateTime.Now;
                        unit.active = true;
                    }
                    else
                    {
                        unit = db.UnitMasters.FirstOrDefault(m => m.unitId == UnitId && m.active == true);

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

        private void TxtUnitName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    if (db.UnitMasters.FirstOrDefault(x => x.unitName.ToLower() == TxtUnitName.Text.Trim().ToLower()) != null)
                    {
                        e.Cancel = true;
                        MessageBox.Show(text: "Unit Name already exists.", caption: "Information", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                int.TryParse(s: Convert.ToString(DgvList["unitId", rowIndex].Value), result: out int unitId);

                using (db = new Inventory_DflowEntities())
                {
                    var unit = db.UnitMasters.FirstOrDefault(x => x.unitId == unitId);

                    if (unit != null)
                    {
                        UnitId = unit.unitId;
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
            UnitId = 0;
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = db.UnitMasters
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
                MessageBox.Show(ex.Message);
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
                        if (DialogResult.No == MessageBox.Show("Are you sure to delete this record ?", "Warning", MessageBoxButtons.YesNo))
                            return;
                        
                        int.TryParse(s: Convert.ToString(value: DgvList["unitId", DgvList.CurrentCell.RowIndex].Value), result: out int unitId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.UnitMasters.FirstOrDefault(x => x.unitId == unitId) != null)
                            {
                                db.UnitMasters.FirstOrDefault(x => x.unitId == unitId).active = false;
                                db.UnitMasters.FirstOrDefault(x => x.unitId == unitId).updatedBy = SessionHelper.UserId;
                                db.UnitMasters.FirstOrDefault(x => x.unitId == unitId).updatedDate = DateTime.Now;

                                db.SaveChanges();

                                Get_Data();
                            }
                            else
                            {
                                MessageBox.Show("Cannot delete this record");
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
