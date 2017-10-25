namespace Dflow_Inventory.ContentPage
{
    partial class PurchaseOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtOrderNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVendorId = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvItems = new System.Windows.Forms.DataGridView();
            this.Col_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchaseDetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LstVendor = new System.Windows.Forms.ListBox();
            this.TxtPONumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtVendorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Link_shorcut = new System.Windows.Forms.LinkLabel();
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvItems)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(842, 494);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblTotalAmount);
            this.tabPage1.Controls.Add(this.txtRemark);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dtpOrderDate);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.TxtOrderNo);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lblVendorId);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.LstVendor);
            this.tabPage1.Controls.Add(this.TxtPONumber);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.BtnCancel);
            this.tabPage1.Controls.Add(this.BtnSave);
            this.tabPage1.Controls.Add(this.TxtVendorName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(834, 460);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add Purchase Order";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Gill Sans MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(637, 378);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(142, 27);
            this.lblTotalAmount.TabIndex = 26;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(145, 95);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(643, 23);
            this.txtRemark.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 24;
            this.label4.Text = "Remark";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.CustomFormat = "dd/MM/yyyy";
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderDate.Location = new System.Drawing.Point(685, 33);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(103, 23);
            this.dtpOrderDate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(634, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 18);
            this.label5.TabIndex = 23;
            this.label5.Text = "Date";
            // 
            // TxtOrderNo
            // 
            this.TxtOrderNo.Location = new System.Drawing.Point(514, 33);
            this.TxtOrderNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtOrderNo.Name = "TxtOrderNo";
            this.TxtOrderNo.Size = new System.Drawing.Size(105, 23);
            this.TxtOrderNo.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "Oder No.";
            // 
            // lblVendorId
            // 
            this.lblVendorId.AutoSize = true;
            this.lblVendorId.Location = new System.Drawing.Point(622, 7);
            this.lblVendorId.Name = "lblVendorId";
            this.lblVendorId.Size = new System.Drawing.Size(40, 18);
            this.lblVendorId.TabIndex = 20;
            this.lblVendorId.Text = "label4";
            this.lblVendorId.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(316, 32);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(107, 23);
            this.dtpDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvItems);
            this.groupBox1.Location = new System.Drawing.Point(36, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 241);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item List";
            // 
            // DgvItems
            // 
            this.DgvItems.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Item,
            this.Col_Unit,
            this.Col_Rate,
            this.Col_Quantity,
            this.Col_Amount,
            this.itemId,
            this.purchaseDetailId});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.DgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvItems.Location = new System.Drawing.Point(3, 19);
            this.DgvItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvItems.Name = "DgvItems";
            this.DgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DgvItems.Size = new System.Drawing.Size(749, 219);
            this.DgvItems.TabIndex = 8;
            this.DgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvItems_CellEndEdit);
            this.DgvItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DgvItems_CellValidating);
            this.DgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgvItems_EditingControlShowing);
            this.DgvItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvItems_RowPostPaint);
            this.DgvItems.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvItems_RowValidating);
            this.DgvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvItems_KeyDown);
            // 
            // Col_Item
            // 
            this.Col_Item.Frozen = true;
            this.Col_Item.HeaderText = "Item Name";
            this.Col_Item.Name = "Col_Item";
            this.Col_Item.Width = 270;
            // 
            // Col_Unit
            // 
            this.Col_Unit.Frozen = true;
            this.Col_Unit.HeaderText = "Unit";
            this.Col_Unit.Name = "Col_Unit";
            this.Col_Unit.ReadOnly = true;
            this.Col_Unit.Width = 80;
            // 
            // Col_Rate
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Col_Rate.DefaultCellStyle = dataGridViewCellStyle1;
            this.Col_Rate.Frozen = true;
            this.Col_Rate.HeaderText = "Rate";
            this.Col_Rate.Name = "Col_Rate";
            this.Col_Rate.Width = 120;
            // 
            // Col_Quantity
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Col_Quantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.Col_Quantity.Frozen = true;
            this.Col_Quantity.HeaderText = "Quantity";
            this.Col_Quantity.Name = "Col_Quantity";
            this.Col_Quantity.Width = 80;
            // 
            // Col_Amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Col_Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Col_Amount.Frozen = true;
            this.Col_Amount.HeaderText = "Amount";
            this.Col_Amount.Name = "Col_Amount";
            this.Col_Amount.Width = 150;
            // 
            // itemId
            // 
            this.itemId.Frozen = true;
            this.itemId.HeaderText = "ItemId";
            this.itemId.Name = "itemId";
            this.itemId.Visible = false;
            // 
            // purchaseDetailId
            // 
            this.purchaseDetailId.HeaderText = "purchaseDetailId";
            this.purchaseDetailId.Name = "purchaseDetailId";
            this.purchaseDetailId.Visible = false;
            // 
            // LstVendor
            // 
            this.LstVendor.FormattingEnabled = true;
            this.LstVendor.ItemHeight = 18;
            this.LstVendor.Location = new System.Drawing.Point(668, 7);
            this.LstVendor.Name = "LstVendor";
            this.LstVendor.Size = new System.Drawing.Size(120, 22);
            this.LstVendor.TabIndex = 16;
            this.LstVendor.Visible = false;
            this.LstVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstVendor_KeyDown);
            this.LstVendor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstVendor_MouseDoubleClick);
            // 
            // TxtPONumber
            // 
            this.TxtPONumber.Location = new System.Drawing.Point(145, 33);
            this.TxtPONumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtPONumber.Name = "TxtPONumber";
            this.TxtPONumber.ReadOnly = true;
            this.TxtPONumber.Size = new System.Drawing.Size(105, 23);
            this.TxtPONumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "P.O. Number";
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(51)))));
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnCancel.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(117, 405);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 30);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(39, 405);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(72, 30);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtVendorName
            // 
            this.TxtVendorName.Location = new System.Drawing.Point(145, 64);
            this.TxtVendorName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtVendorName.Name = "TxtVendorName";
            this.TxtVendorName.Size = new System.Drawing.Size(313, 23);
            this.TxtVendorName.TabIndex = 5;
            this.TxtVendorName.TextChanged += new System.EventHandler(this.TxtVendorName_TextChanged);
            this.TxtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVendorName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vendor Name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Link_shorcut);
            this.tabPage2.Controls.Add(this.DgvList);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(834, 460);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Purchase Order";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Link_shorcut
            // 
            this.Link_shorcut.AutoSize = true;
            this.Link_shorcut.Dock = System.Windows.Forms.DockStyle.Right;
            this.Link_shorcut.Location = new System.Drawing.Point(739, 4);
            this.Link_shorcut.Name = "Link_shorcut";
            this.Link_shorcut.Size = new System.Drawing.Size(92, 18);
            this.Link_shorcut.TabIndex = 1;
            this.Link_shorcut.TabStop = true;
            this.Link_shorcut.Text = "Show Shortcuts";
            // 
            // DgvList
            // 
            this.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvList.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DgvList.Location = new System.Drawing.Point(3, 26);
            this.DgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvList.Name = "DgvList";
            this.DgvList.ReadOnly = true;
            this.DgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvList.Size = new System.Drawing.Size(828, 430);
            this.DgvList.TabIndex = 0;
            this.DgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvList_CellDoubleClick);
            this.DgvList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvList_RowPostPaint);
            this.DgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvList_KeyDown);
            // 
            // PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 494);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PurchaseOrder";
            this.Text = "PurchaseOrder";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvItems)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox LstVendor;
        private System.Windows.Forms.TextBox TxtPONumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtVendorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel Link_shorcut;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.DataGridView DgvItems;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblVendorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseDetailId;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalAmount;
    }
}