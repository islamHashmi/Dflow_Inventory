namespace Dflow_Inventory.ContentPage
{
    partial class VoucherPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.LstCustomer = new System.Windows.Forms.ListBox();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.DtpChqDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtChqueNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtBankName = new System.Windows.Forms.TextBox();
            this.TxtNarration = new System.Windows.Forms.TextBox();
            this.lblNaration = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpVoucherDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtVoucherNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Link_shorcut = new System.Windows.Forms.LinkLabel();
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.cmbExpenseHead = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpPayment.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(899, 435);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.cmbEmployee);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.cmbExpenseHead);
            this.tabPage1.Controls.Add(this.lblCustomerId);
            this.tabPage1.Controls.Add(this.LstCustomer);
            this.tabPage1.Controls.Add(this.grpPayment);
            this.tabPage1.Controls.Add(this.TxtNarration);
            this.tabPage1.Controls.Add(this.lblNaration);
            this.tabPage1.Controls.Add(this.BtnSave);
            this.tabPage1.Controls.Add(this.TxtAmount);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.CmbPaymentMode);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.DtpVoucherDate);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.TxtVoucherNo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BtnCancel);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(891, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add Payment";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Location = new System.Drawing.Point(736, 9);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(40, 18);
            this.lblCustomerId.TabIndex = 33;
            this.lblCustomerId.Text = "label4";
            this.lblCustomerId.Visible = false;
            // 
            // LstCustomer
            // 
            this.LstCustomer.FormattingEnabled = true;
            this.LstCustomer.ItemHeight = 18;
            this.LstCustomer.Location = new System.Drawing.Point(782, 7);
            this.LstCustomer.Name = "LstCustomer";
            this.LstCustomer.Size = new System.Drawing.Size(103, 22);
            this.LstCustomer.TabIndex = 32;
            this.LstCustomer.Visible = false;
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.DtpChqDate);
            this.grpPayment.Controls.Add(this.label8);
            this.grpPayment.Controls.Add(this.label7);
            this.grpPayment.Controls.Add(this.TxtChqueNo);
            this.grpPayment.Controls.Add(this.label6);
            this.grpPayment.Controls.Add(this.TxtBankName);
            this.grpPayment.Location = new System.Drawing.Point(35, 223);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(414, 121);
            this.grpPayment.TabIndex = 29;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Payment Details";
            this.grpPayment.Visible = false;
            // 
            // DtpChqDate
            // 
            this.DtpChqDate.CustomFormat = "dd/MM/yyyy";
            this.DtpChqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpChqDate.Location = new System.Drawing.Point(94, 86);
            this.DtpChqDate.Name = "DtpChqDate";
            this.DtpChqDate.Size = new System.Drawing.Size(114, 23);
            this.DtpChqDate.TabIndex = 34;
            this.DtpChqDate.ValueChanged += new System.EventHandler(this.DtpChqDate_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 33;
            this.label8.Text = "Cheque Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 18);
            this.label7.TabIndex = 31;
            this.label7.Text = "Cheque No.";
            // 
            // TxtChqueNo
            // 
            this.TxtChqueNo.Location = new System.Drawing.Point(94, 57);
            this.TxtChqueNo.MaxLength = 6;
            this.TxtChqueNo.Name = "TxtChqueNo";
            this.TxtChqueNo.Size = new System.Drawing.Size(114, 23);
            this.TxtChqueNo.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Bank Name";
            // 
            // TxtBankName
            // 
            this.TxtBankName.Location = new System.Drawing.Point(94, 28);
            this.TxtBankName.Name = "TxtBankName";
            this.TxtBankName.Size = new System.Drawing.Size(314, 23);
            this.TxtBankName.TabIndex = 28;
            // 
            // TxtNarration
            // 
            this.TxtNarration.Location = new System.Drawing.Point(472, 88);
            this.TxtNarration.Multiline = true;
            this.TxtNarration.Name = "TxtNarration";
            this.TxtNarration.Size = new System.Drawing.Size(391, 139);
            this.TxtNarration.TabIndex = 31;
            // 
            // lblNaration
            // 
            this.lblNaration.AutoSize = true;
            this.lblNaration.Location = new System.Drawing.Point(469, 62);
            this.lblNaration.Name = "lblNaration";
            this.lblNaration.Size = new System.Drawing.Size(61, 18);
            this.lblNaration.TabIndex = 30;
            this.lblNaration.Text = "Narration";
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(472, 258);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(72, 30);
            this.BtnSave.TabIndex = 28;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtAmount
            // 
            this.TxtAmount.Location = new System.Drawing.Point(154, 153);
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new System.Drawing.Size(113, 23);
            this.TxtAmount.TabIndex = 27;
            this.TxtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAmount_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "Amount";
            // 
            // CmbPaymentMode
            // 
            this.CmbPaymentMode.FormattingEnabled = true;
            this.CmbPaymentMode.Location = new System.Drawing.Point(154, 182);
            this.CmbPaymentMode.Name = "CmbPaymentMode";
            this.CmbPaymentMode.Size = new System.Drawing.Size(165, 26);
            this.CmbPaymentMode.TabIndex = 25;
            this.CmbPaymentMode.SelectionChangeCommitted += new System.EventHandler(this.CmbPaymentMode_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "Payment Mode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "Expense Head";
            // 
            // DtpVoucherDate
            // 
            this.DtpVoucherDate.CustomFormat = "dd/MM/yyyy";
            this.DtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpVoucherDate.Location = new System.Drawing.Point(338, 29);
            this.DtpVoucherDate.Name = "DtpVoucherDate";
            this.DtpVoucherDate.Size = new System.Drawing.Size(104, 23);
            this.DtpVoucherDate.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Date";
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.Location = new System.Drawing.Point(154, 30);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.ReadOnly = true;
            this.TxtVoucherNo.Size = new System.Drawing.Size(113, 23);
            this.TxtVoucherNo.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Voucher No.";
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(51)))));
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnCancel.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(550, 258);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 30);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Link_shorcut);
            this.tabPage2.Controls.Add(this.DgvList);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(891, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Payments";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Link_shorcut
            // 
            this.Link_shorcut.AutoSize = true;
            this.Link_shorcut.Dock = System.Windows.Forms.DockStyle.Right;
            this.Link_shorcut.Location = new System.Drawing.Point(796, 4);
            this.Link_shorcut.Name = "Link_shorcut";
            this.Link_shorcut.Size = new System.Drawing.Size(92, 18);
            this.Link_shorcut.TabIndex = 2;
            this.Link_shorcut.TabStop = true;
            this.Link_shorcut.Text = "Show Shortcuts";
            // 
            // DgvList
            // 
            this.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DgvList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvList.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DgvList.Location = new System.Drawing.Point(3, 26);
            this.DgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvList.Name = "DgvList";
            this.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvList.Size = new System.Drawing.Size(885, 371);
            this.DgvList.TabIndex = 0;
            this.DgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvList_CellDoubleClick);
            this.DgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvList_KeyDown);
            // 
            // cmbExpenseHead
            // 
            this.cmbExpenseHead.FormattingEnabled = true;
            this.cmbExpenseHead.Location = new System.Drawing.Point(154, 59);
            this.cmbExpenseHead.Name = "cmbExpenseHead";
            this.cmbExpenseHead.Size = new System.Drawing.Size(288, 26);
            this.cmbExpenseHead.TabIndex = 34;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(154, 124);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 23);
            this.txtName.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 18);
            this.label9.TabIndex = 35;
            this.label9.Text = "Employee";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(154, 92);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(288, 26);
            this.cmbEmployee.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 18);
            this.label10.TabIndex = 38;
            this.label10.Text = "Name";
            // 
            // VoucherPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 435);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VoucherPayment";
            this.Text = "Payment Voucher";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbExpenseHead;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.ListBox LstCustomer;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.DateTimePicker DtpChqDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtChqueNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtBankName;
        private System.Windows.Forms.TextBox TxtNarration;
        private System.Windows.Forms.Label lblNaration;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CmbPaymentMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpVoucherDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtVoucherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel Link_shorcut;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label label10;
    }
}