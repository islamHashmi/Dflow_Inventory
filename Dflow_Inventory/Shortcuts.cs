using System;
using System.Data;
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

        private DataTable Create_DataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("control", typeof(string));
            dt.Columns.Add("key", typeof(string));

            dt.Rows.Add("Delete", "[Delete]");
            dt.Rows.Add("Edit", "[Enter]");
            dt.Rows.Add("Save", "[Ctrl + S]");
            dt.Rows.Add("Clear", "[Ctrl + C]");
            dt.Rows.Add("Undo", "[Ctrl + Z]");
            dt.Rows.Add("Redo", "[Ctrl + U]");

            return dt;
        }

        private void Short_Cut()
        {
            DataTable dt = Create_DataTable();
            
            int col = 0, row = 0;

            foreach(DataRow dr in dt.Rows)
            {
                if (col == 3)
                {
                    col = 0;
                    row++;
                }
                
                new Label().Text = Convert.ToString(dr["control"]);

                tableLayoutPanel1.Controls.Add(new Label(), col, row);

                col++;
                
                new Label().Text = Convert.ToString(dr["key"]);

                tableLayoutPanel1.Controls.Add(new Label(), col, row);

                col++;
            }            
        }        
    }
}
