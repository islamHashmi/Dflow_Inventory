using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dflow_Inventory
{
    public partial class Shortcuts : Form
    {
        public Shortcuts()
        {
            InitializeComponent();

            Short_Cut();
        }

        private void Short_Cut()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Control", typeof(string));
            dt.Columns.Add("Shortcut", typeof(string));
            dt.Columns.Add("Control1", typeof(string));
            dt.Columns.Add("Shortcut1", typeof(string));

            DataRow row = dt.NewRow();

            row["Control"] = "Delete";
            row["Shortcut"] = "[Delete]";
            row["Control1"] = "Edit";
            row["Shortcut1"] = "[Enter]";

            dt.Rows.Add(row);

            dgvList.DataSource = dt;

            Set_Column();
        }

        private void Set_Column()
        {
            dgvList.Columns["Control"].HeaderText = "Control";
            dgvList.Columns["Shortcut"].HeaderText = "Shortcut Key";
            dgvList.Columns["Control1"].HeaderText = "Control";
            dgvList.Columns["Shortcut1"].HeaderText = "Shortcut Key";
        }
    }
}
