namespace Dflow_Inventory.ContentPage
{
    partial class UserMaster
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtLoginId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Link_shorcut = new System.Windows.Forms.LinkLabel();
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.TxtConfirmPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtMobile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbUserGroup = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(662, 416);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbUserGroup);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.TxtEmail);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.TxtMobile);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.TxtName);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.TxtConfirmPass);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.TxtPassword);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BtnCancel);
            this.tabPage1.Controls.Add(this.BtnSave);
            this.tabPage1.Controls.Add(this.TxtLoginId);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(654, 382);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add User";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(199, 65);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(156, 23);
            this.TxtPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Password";
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(51)))));
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnCancel.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(277, 272);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 30);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.BtnSave.Font = new System.Drawing.Font("Gill Sans MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(199, 272);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(72, 30);
            this.BtnSave.TabIndex = 8;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtLoginId
            // 
            this.TxtLoginId.Location = new System.Drawing.Point(199, 34);
            this.TxtLoginId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtLoginId.Name = "TxtLoginId";
            this.TxtLoginId.Size = new System.Drawing.Size(156, 23);
            this.TxtLoginId.TabIndex = 1;
            this.TxtLoginId.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLoginId_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Login Id";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Link_shorcut);
            this.tabPage2.Controls.Add(this.DgvList);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(654, 382);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Users";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Link_shorcut
            // 
            this.Link_shorcut.AutoSize = true;
            this.Link_shorcut.Dock = System.Windows.Forms.DockStyle.Right;
            this.Link_shorcut.Location = new System.Drawing.Point(559, 4);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvList.DefaultCellStyle = dataGridViewCellStyle4;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DgvList.Location = new System.Drawing.Point(3, 26);
            this.DgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvList.Name = "DgvList";
            this.DgvList.ReadOnly = true;
            this.DgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvList.Size = new System.Drawing.Size(648, 352);
            this.DgvList.TabIndex = 0;
            this.DgvList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvList_RowPostPaint);
            // 
            // TxtConfirmPass
            // 
            this.TxtConfirmPass.Location = new System.Drawing.Point(199, 96);
            this.TxtConfirmPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtConfirmPass.Name = "TxtConfirmPass";
            this.TxtConfirmPass.Size = new System.Drawing.Size(156, 23);
            this.TxtConfirmPass.TabIndex = 3;
            this.TxtConfirmPass.Validating += new System.ComponentModel.CancelEventHandler(this.TxtConfirmPass_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Confirm Password";
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(199, 160);
            this.TxtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(314, 23);
            this.TxtName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Name";
            // 
            // TxtMobile
            // 
            this.TxtMobile.Location = new System.Drawing.Point(199, 191);
            this.TxtMobile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtMobile.MaxLength = 10;
            this.TxtMobile.Name = "TxtMobile";
            this.TxtMobile.Size = new System.Drawing.Size(156, 23);
            this.TxtMobile.TabIndex = 6;
            this.TxtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMobile_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mobile No.";
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(199, 222);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(367, 23);
            this.TxtEmail.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Email Id";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "User Group";
            // 
            // cmbUserGroup
            // 
            this.cmbUserGroup.FormattingEnabled = true;
            this.cmbUserGroup.Location = new System.Drawing.Point(199, 127);
            this.cmbUserGroup.Name = "cmbUserGroup";
            this.cmbUserGroup.Size = new System.Drawing.Size(187, 26);
            this.cmbUserGroup.TabIndex = 4;
            // 
            // UserMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 416);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserMaster";
            this.Text = "User Master";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbUserGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtMobile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtConfirmPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtLoginId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel Link_shorcut;
        private System.Windows.Forms.DataGridView DgvList;
    }
}