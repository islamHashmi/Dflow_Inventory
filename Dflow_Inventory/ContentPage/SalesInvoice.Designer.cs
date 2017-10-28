namespace Dflow_Inventory.ContentPage
{
    partial class SalesInvoice
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
            this.label12 = new System.Windows.Forms.Label();
            this.TxtTotalAmt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtSGST = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtSGSTAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtCGST = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtCGSTAmt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtTaxableAmt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvItems = new System.Windows.Forms.DataGridView();
            this.Col_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_HSNCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceDetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LstCustomer = new System.Windows.Forms.ListBox();
            this.TxtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtCustomerName = new System.Windows.Forms.TextBox();
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
            this.tabControl1.Size = new System.Drawing.Size(868, 526);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.TxtTotalAmt);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.TxtSGST);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.TxtSGSTAmt);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.TxtCGST);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.TxtCGSTAmt);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.TxtTaxableAmt);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.TxtDiscount);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.TxtTotal);
            this.tabPage1.Controls.Add(this.txtRemark);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblCustomerId);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.LstCustomer);
            this.tabPage1.Controls.Add(this.TxtInvoiceNo);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.BtnCancel);
            this.tabPage1.Controls.Add(this.BtnSave);
            this.tabPage1.Controls.Add(this.TxtCustomerName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(860, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add Invoice";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(535, 451);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 21);
            this.label12.TabIndex = 41;
            this.label12.Text = "Total Amount";
            // 
            // TxtTotalAmt
            // 
            this.TxtTotalAmt.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotalAmt.Location = new System.Drawing.Point(652, 448);
            this.TxtTotalAmt.Name = "TxtTotalAmt";
            this.TxtTotalAmt.ReadOnly = true;
            this.TxtTotalAmt.Size = new System.Drawing.Size(146, 25);
            this.TxtTotalAmt.TabIndex = 40;
            this.TxtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(625, 420);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 21);
            this.label10.TabIndex = 39;
            this.label10.Text = "%";
            // 
            // TxtSGST
            // 
            this.TxtSGST.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSGST.Location = new System.Drawing.Point(578, 417);
            this.TxtSGST.Name = "TxtSGST";
            this.TxtSGST.Size = new System.Drawing.Size(46, 25);
            this.TxtSGST.TabIndex = 14;
            this.TxtSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSGST.TextChanged += new System.EventHandler(this.TxtSGST_TextChanged);
            this.TxtSGST.Leave += new System.EventHandler(this.TxtSGST_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(486, 420);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 21);
            this.label11.TabIndex = 37;
            this.label11.Text = "Add SGST";
            // 
            // TxtSGSTAmt
            // 
            this.TxtSGSTAmt.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSGSTAmt.Location = new System.Drawing.Point(652, 417);
            this.TxtSGSTAmt.Name = "TxtSGSTAmt";
            this.TxtSGSTAmt.ReadOnly = true;
            this.TxtSGSTAmt.Size = new System.Drawing.Size(146, 25);
            this.TxtSGSTAmt.TabIndex = 36;
            this.TxtSGSTAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(625, 389);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 21);
            this.label9.TabIndex = 35;
            this.label9.Text = "%";
            // 
            // TxtCGST
            // 
            this.TxtCGST.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCGST.Location = new System.Drawing.Point(578, 386);
            this.TxtCGST.Name = "TxtCGST";
            this.TxtCGST.Size = new System.Drawing.Size(46, 25);
            this.TxtCGST.TabIndex = 11;
            this.TxtCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCGST.TextChanged += new System.EventHandler(this.TxtCGST_TextChanged);
            this.TxtCGST.Leave += new System.EventHandler(this.TxtCGST_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(486, 389);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 21);
            this.label8.TabIndex = 33;
            this.label8.Text = "Add CGST";
            // 
            // TxtCGSTAmt
            // 
            this.TxtCGSTAmt.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCGSTAmt.Location = new System.Drawing.Point(652, 386);
            this.TxtCGSTAmt.Name = "TxtCGSTAmt";
            this.TxtCGSTAmt.ReadOnly = true;
            this.TxtCGSTAmt.Size = new System.Drawing.Size(146, 25);
            this.TxtCGSTAmt.TabIndex = 32;
            this.TxtCGSTAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(517, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 21);
            this.label7.TabIndex = 31;
            this.label7.Text = "Taxable Amount";
            // 
            // TxtTaxableAmt
            // 
            this.TxtTaxableAmt.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTaxableAmt.Location = new System.Drawing.Point(652, 355);
            this.TxtTaxableAmt.Name = "TxtTaxableAmt";
            this.TxtTaxableAmt.ReadOnly = true;
            this.TxtTaxableAmt.Size = new System.Drawing.Size(146, 25);
            this.TxtTaxableAmt.TabIndex = 30;
            this.TxtTaxableAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(540, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 21);
            this.label6.TabIndex = 29;
            this.label6.Text = "Less Discount";
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiscount.Location = new System.Drawing.Point(652, 324);
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(146, 25);
            this.TxtDiscount.TabIndex = 9;
            this.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.TxtDiscount.Leave += new System.EventHandler(this.TxtDiscount_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(598, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 21);
            this.label5.TabIndex = 27;
            this.label5.Text = "Total";
            // 
            // TxtTotal
            // 
            this.TxtTotal.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotal.Location = new System.Drawing.Point(652, 293);
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.ReadOnly = true;
            this.TxtTotal.Size = new System.Drawing.Size(146, 25);
            this.TxtTotal.TabIndex = 26;
            this.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(39, 319);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(417, 116);
            this.txtRemark.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 24;
            this.label4.Text = "Remark";
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Location = new System.Drawing.Point(814, 30);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(40, 18);
            this.lblCustomerId.TabIndex = 20;
            this.lblCustomerId.Text = "label4";
            this.lblCustomerId.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(260, 17);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(107, 23);
            this.dtpDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvItems);
            this.groupBox1.Location = new System.Drawing.Point(36, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 241);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item List";
            // 
            // DgvItems
            // 
            this.DgvItems.AllowUserToDeleteRows = false;
            this.DgvItems.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Item,
            this.Col_HSNCode,
            this.Col_Rate,
            this.Col_Quantity,
            this.Col_Amount,
            this.itemId,
            this.invoiceDetailId,
            this.Col_Unit});
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
            this.DgvItems.Size = new System.Drawing.Size(770, 219);
            this.DgvItems.TabIndex = 6;
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
            // Col_HSNCode
            // 
            this.Col_HSNCode.HeaderText = "HSN Code";
            this.Col_HSNCode.Name = "Col_HSNCode";
            // 
            // Col_Rate
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Col_Rate.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.Col_Amount.HeaderText = "Amount";
            this.Col_Amount.Name = "Col_Amount";
            this.Col_Amount.Width = 150;
            // 
            // itemId
            // 
            this.itemId.HeaderText = "ItemId";
            this.itemId.Name = "itemId";
            this.itemId.Visible = false;
            // 
            // invoiceDetailId
            // 
            this.invoiceDetailId.HeaderText = "invoiceDetailId";
            this.invoiceDetailId.Name = "invoiceDetailId";
            this.invoiceDetailId.Visible = false;
            // 
            // Col_Unit
            // 
            this.Col_Unit.HeaderText = "Unit";
            this.Col_Unit.Name = "Col_Unit";
            this.Col_Unit.ReadOnly = true;
            this.Col_Unit.Visible = false;
            this.Col_Unit.Width = 80;
            // 
            // LstCustomer
            // 
            this.LstCustomer.FormattingEnabled = true;
            this.LstCustomer.ItemHeight = 18;
            this.LstCustomer.Location = new System.Drawing.Point(810, 5);
            this.LstCustomer.Name = "LstCustomer";
            this.LstCustomer.Size = new System.Drawing.Size(47, 22);
            this.LstCustomer.TabIndex = 16;
            this.LstCustomer.Visible = false;
            this.LstCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstCustomer_KeyDown);
            this.LstCustomer.Leave += new System.EventHandler(this.LstCustomer_Leave);
            this.LstCustomer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstCustomer_MouseDoubleClick);
            // 
            // TxtInvoiceNo
            // 
            this.TxtInvoiceNo.Location = new System.Drawing.Point(109, 19);
            this.TxtInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtInvoiceNo.Name = "TxtInvoiceNo";
            this.TxtInvoiceNo.ReadOnly = true;
            this.TxtInvoiceNo.Size = new System.Drawing.Size(105, 23);
            this.TxtInvoiceNo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Invoice No.";
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(51)))));
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnCancel.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(117, 446);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 30);
            this.BtnCancel.TabIndex = 17;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(39, 446);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(72, 30);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtCustomerName
            // 
            this.TxtCustomerName.Location = new System.Drawing.Point(476, 19);
            this.TxtCustomerName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtCustomerName.Name = "TxtCustomerName";
            this.TxtCustomerName.Size = new System.Drawing.Size(333, 23);
            this.TxtCustomerName.TabIndex = 5;
            this.TxtCustomerName.TextChanged += new System.EventHandler(this.TxtCustomerName_TextChanged);
            this.TxtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomerName_KeyDown_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Customer Name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Link_shorcut);
            this.tabPage2.Controls.Add(this.DgvList);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(860, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Invoices";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Link_shorcut
            // 
            this.Link_shorcut.AutoSize = true;
            this.Link_shorcut.Dock = System.Windows.Forms.DockStyle.Right;
            this.Link_shorcut.Location = new System.Drawing.Point(765, 4);
            this.Link_shorcut.Name = "Link_shorcut";
            this.Link_shorcut.Size = new System.Drawing.Size(92, 18);
            this.Link_shorcut.TabIndex = 1;
            this.Link_shorcut.TabStop = true;
            this.Link_shorcut.Text = "Show Shortcuts";
            // 
            // DgvList
            // 
            this.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.DgvList.Size = new System.Drawing.Size(854, 462);
            this.DgvList.TabIndex = 0;
            this.DgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvList_CellDoubleClick);
            this.DgvList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvList_RowPostPaint);
            this.DgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvList_KeyDown);
            // 
            // SalesInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 526);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SalesInvoice";
            this.Text = "Sales Invoice";
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
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvItems;
        private System.Windows.Forms.ListBox LstCustomer;
        private System.Windows.Forms.TextBox TxtInvoiceNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtCustomerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel Link_shorcut;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TxtTotalAmt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtSGST;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtSGSTAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtCGST;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtCGSTAmt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtTaxableAmt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_HSNCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceDetailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Unit;
    }
}