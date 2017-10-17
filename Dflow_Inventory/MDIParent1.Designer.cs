namespace Dflow_Inventory
{
    partial class MDIParent1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent1));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripLoginUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripFinYear = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodReceiptNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderBookingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voucherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLoginUser,
            this.toolStripFinYear});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(942, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripLoginUser
            // 
            this.toolStripLoginUser.BackColor = System.Drawing.Color.BurlyWood;
            this.toolStripLoginUser.Name = "toolStripLoginUser";
            this.toolStripLoginUser.Size = new System.Drawing.Size(118, 17);
            this.toolStripLoginUser.Text = "toolStripStatusLabel1";
            // 
            // toolStripFinYear
            // 
            this.toolStripFinYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(153)))));
            this.toolStripFinYear.Name = "toolStripFinYear";
            this.toolStripFinYear.Size = new System.Drawing.Size(118, 17);
            this.toolStripFinYear.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Gill Sans MT", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterToolStripMenuItem,
            this.purchaseToolStripMenuItem,
            this.goodReceiptNoteToolStripMenuItem,
            this.salesToolStripMenuItem,
            this.voucherToolStripMenuItem,
            this.securityToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(942, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemsToolStripMenuItem,
            this.unitsToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.designationToolStripMenuItem,
            this.employeeToolStripMenuItem});
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(72, 29);
            this.masterToolStripMenuItem.Text = "Master";
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
            this.itemsToolStripMenuItem.Text = "Items";
            this.itemsToolStripMenuItem.Click += new System.EventHandler(this.itemsToolStripMenuItem_Click);
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
            this.unitsToolStripMenuItem.Text = "Units";
            this.unitsToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
            this.customersToolStripMenuItem.Text = "Customers";
            this.customersToolStripMenuItem.Click += new System.EventHandler(this.customersToolStripMenuItem_Click);
            // 
            // designationToolStripMenuItem
            // 
            this.designationToolStripMenuItem.Name = "designationToolStripMenuItem";
            this.designationToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
            this.designationToolStripMenuItem.Text = "Designation";
            this.designationToolStripMenuItem.Click += new System.EventHandler(this.designationToolStripMenuItem_Click);
            // 
            // employeeToolStripMenuItem
            // 
            this.employeeToolStripMenuItem.Name = "employeeToolStripMenuItem";
            this.employeeToolStripMenuItem.Size = new System.Drawing.Size(168, 30);
            this.employeeToolStripMenuItem.Text = "Employee";
            this.employeeToolStripMenuItem.Click += new System.EventHandler(this.employeeToolStripMenuItem_Click);
            // 
            // purchaseToolStripMenuItem
            // 
            this.purchaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseOrderToolStripMenuItem});
            this.purchaseToolStripMenuItem.Name = "purchaseToolStripMenuItem";
            this.purchaseToolStripMenuItem.Size = new System.Drawing.Size(87, 29);
            this.purchaseToolStripMenuItem.Text = "Purchase";
            // 
            // purchaseOrderToolStripMenuItem
            // 
            this.purchaseOrderToolStripMenuItem.Name = "purchaseOrderToolStripMenuItem";
            this.purchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(147, 30);
            this.purchaseOrderToolStripMenuItem.Text = "Purchase";
            this.purchaseOrderToolStripMenuItem.Click += new System.EventHandler(this.purchaseOrderToolStripMenuItem_Click);
            // 
            // goodReceiptNoteToolStripMenuItem
            // 
            this.goodReceiptNoteToolStripMenuItem.Name = "goodReceiptNoteToolStripMenuItem";
            this.goodReceiptNoteToolStripMenuItem.Size = new System.Drawing.Size(162, 29);
            this.goodReceiptNoteToolStripMenuItem.Text = "Good Receipt Note";
            // 
            // salesToolStripMenuItem
            // 
            this.salesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderBookingToolStripMenuItem,
            this.salesInvoiceToolStripMenuItem});
            this.salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            this.salesToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.salesToolStripMenuItem.Text = "Sales";
            // 
            // orderBookingToolStripMenuItem
            // 
            this.orderBookingToolStripMenuItem.Name = "orderBookingToolStripMenuItem";
            this.orderBookingToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.orderBookingToolStripMenuItem.Text = "Order Booking";
            // 
            // salesInvoiceToolStripMenuItem
            // 
            this.salesInvoiceToolStripMenuItem.Name = "salesInvoiceToolStripMenuItem";
            this.salesInvoiceToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.salesInvoiceToolStripMenuItem.Text = "Sales Invoice";
            this.salesInvoiceToolStripMenuItem.Click += new System.EventHandler(this.salesInvoiceToolStripMenuItem_Click);
            // 
            // voucherToolStripMenuItem
            // 
            this.voucherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receiptToolStripMenuItem,
            this.paymentToolStripMenuItem});
            this.voucherToolStripMenuItem.Name = "voucherToolStripMenuItem";
            this.voucherToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.voucherToolStripMenuItem.Text = "Voucher";
            // 
            // receiptToolStripMenuItem
            // 
            this.receiptToolStripMenuItem.Name = "receiptToolStripMenuItem";
            this.receiptToolStripMenuItem.Size = new System.Drawing.Size(143, 30);
            this.receiptToolStripMenuItem.Text = "Receipt";
            // 
            // paymentToolStripMenuItem
            // 
            this.paymentToolStripMenuItem.Name = "paymentToolStripMenuItem";
            this.paymentToolStripMenuItem.Size = new System.Drawing.Size(143, 30);
            this.paymentToolStripMenuItem.Text = "Payment";
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userMasterToolStripMenuItem,
            this.changePasswordToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(80, 29);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // userMasterToolStripMenuItem
            // 
            this.userMasterToolStripMenuItem.Name = "userMasterToolStripMenuItem";
            this.userMasterToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.userMasterToolStripMenuItem.Text = "User Master";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(73, 29);
            this.logoutToolStripMenuItem.Text = "Logout";
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 523);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDIParent1";
            this.Text = "Dflow Inventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDIParent1_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodReceiptNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderBookingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voucherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem designationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripFinYear;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoginUser;
    }
}



