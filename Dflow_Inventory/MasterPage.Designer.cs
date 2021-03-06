﻿namespace Dflow_Inventory
{
    partial class MasterPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPage));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voucherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voucherReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationParameterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripFinYear = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoginUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.productionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Gill Sans MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mastersToolStripMenuItem,
            this.purchaseToolStripMenuItem,
            this.productionToolStripMenuItem,
            this.salesToolStripMenuItem,
            this.voucherToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.securityToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(949, 31);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mastersToolStripMenuItem
            // 
            this.mastersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitsToolStripMenuItem,
            this.itemsToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.designationsToolStripMenuItem,
            this.employeesToolStripMenuItem,
            this.expensesToolStripMenuItem});
            this.mastersToolStripMenuItem.Name = "mastersToolStripMenuItem";
            this.mastersToolStripMenuItem.Size = new System.Drawing.Size(73, 27);
            this.mastersToolStripMenuItem.Text = "Masters";
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.Image = global::Dflow_Inventory.Properties.Resources.unit_icon;
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.unitsToolStripMenuItem.Text = "Units";
            this.unitsToolStripMenuItem.Click += new System.EventHandler(this.UnitsToolStripMenuItem_Click);
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("itemsToolStripMenuItem.Image")));
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.itemsToolStripMenuItem.Text = "Items";
            this.itemsToolStripMenuItem.Click += new System.EventHandler(this.ItemsToolStripMenuItem_Click);
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.Image = global::Dflow_Inventory.Properties.Resources.customer_icon;
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.customersToolStripMenuItem.Text = "Customers";
            this.customersToolStripMenuItem.Click += new System.EventHandler(this.CustomersToolStripMenuItem_Click);
            // 
            // designationsToolStripMenuItem
            // 
            this.designationsToolStripMenuItem.Image = global::Dflow_Inventory.Properties.Resources.designation_icon;
            this.designationsToolStripMenuItem.Name = "designationsToolStripMenuItem";
            this.designationsToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.designationsToolStripMenuItem.Text = "Designations";
            this.designationsToolStripMenuItem.Click += new System.EventHandler(this.DesignationsToolStripMenuItem_Click);
            // 
            // employeesToolStripMenuItem
            // 
            this.employeesToolStripMenuItem.Image = global::Dflow_Inventory.Properties.Resources.employee_icon;
            this.employeesToolStripMenuItem.Name = "employeesToolStripMenuItem";
            this.employeesToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.employeesToolStripMenuItem.Text = "Employees";
            this.employeesToolStripMenuItem.Click += new System.EventHandler(this.EmployeesToolStripMenuItem_Click);
            // 
            // expensesToolStripMenuItem
            // 
            this.expensesToolStripMenuItem.Image = global::Dflow_Inventory.Properties.Resources.expense_icon;
            this.expensesToolStripMenuItem.Name = "expensesToolStripMenuItem";
            this.expensesToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.expensesToolStripMenuItem.Text = "Expenses";
            this.expensesToolStripMenuItem.Click += new System.EventHandler(this.ExpensesToolStripMenuItem_Click);
            // 
            // purchaseToolStripMenuItem
            // 
            this.purchaseToolStripMenuItem.Name = "purchaseToolStripMenuItem";
            this.purchaseToolStripMenuItem.Size = new System.Drawing.Size(80, 27);
            this.purchaseToolStripMenuItem.Text = "Purchase";
            this.purchaseToolStripMenuItem.Click += new System.EventHandler(this.PurchaseToolStripMenuItem_Click);
            // 
            // productionToolStripMenuItem
            // 
            this.productionToolStripMenuItem.Name = "productionToolStripMenuItem";
            this.productionToolStripMenuItem.Size = new System.Drawing.Size(94, 27);
            this.productionToolStripMenuItem.Text = "Production";
            this.productionToolStripMenuItem.Click += new System.EventHandler(this.productionToolStripMenuItem_Click);
            // 
            // salesToolStripMenuItem
            // 
            this.salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            this.salesToolStripMenuItem.Size = new System.Drawing.Size(54, 27);
            this.salesToolStripMenuItem.Text = "Sales";
            this.salesToolStripMenuItem.Click += new System.EventHandler(this.SalesToolStripMenuItem_Click);
            // 
            // voucherToolStripMenuItem
            // 
            this.voucherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receiptToolStripMenuItem,
            this.paymentToolStripMenuItem});
            this.voucherToolStripMenuItem.Name = "voucherToolStripMenuItem";
            this.voucherToolStripMenuItem.Size = new System.Drawing.Size(78, 27);
            this.voucherToolStripMenuItem.Text = "Voucher";
            // 
            // receiptToolStripMenuItem
            // 
            this.receiptToolStripMenuItem.Name = "receiptToolStripMenuItem";
            this.receiptToolStripMenuItem.Size = new System.Drawing.Size(135, 28);
            this.receiptToolStripMenuItem.Text = "Receipt";
            this.receiptToolStripMenuItem.Click += new System.EventHandler(this.ReceiptToolStripMenuItem_Click);
            // 
            // paymentToolStripMenuItem
            // 
            this.paymentToolStripMenuItem.Name = "paymentToolStripMenuItem";
            this.paymentToolStripMenuItem.Size = new System.Drawing.Size(135, 28);
            this.paymentToolStripMenuItem.Text = "Payment";
            this.paymentToolStripMenuItem.Click += new System.EventHandler(this.PaymentToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyReportToolStripMenuItem,
            this.stockRegisterToolStripMenuItem,
            this.salesReportToolStripMenuItem,
            this.purchaseReportToolStripMenuItem,
            this.productionReportToolStripMenuItem,
            this.voucherReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(74, 27);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // dailyReportToolStripMenuItem
            // 
            this.dailyReportToolStripMenuItem.Name = "dailyReportToolStripMenuItem";
            this.dailyReportToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.dailyReportToolStripMenuItem.Text = "Daily Report";
            this.dailyReportToolStripMenuItem.Click += new System.EventHandler(this.dailyReportToolStripMenuItem_Click);
            // 
            // stockRegisterToolStripMenuItem
            // 
            this.stockRegisterToolStripMenuItem.Name = "stockRegisterToolStripMenuItem";
            this.stockRegisterToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.stockRegisterToolStripMenuItem.Text = "Stock Register";
            this.stockRegisterToolStripMenuItem.Click += new System.EventHandler(this.StockRegisterToolStripMenuItem_Click);
            // 
            // salesReportToolStripMenuItem
            // 
            this.salesReportToolStripMenuItem.Name = "salesReportToolStripMenuItem";
            this.salesReportToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.salesReportToolStripMenuItem.Text = "Sales Report";
            this.salesReportToolStripMenuItem.Click += new System.EventHandler(this.salesReportToolStripMenuItem_Click);
            // 
            // purchaseReportToolStripMenuItem
            // 
            this.purchaseReportToolStripMenuItem.Name = "purchaseReportToolStripMenuItem";
            this.purchaseReportToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.purchaseReportToolStripMenuItem.Text = "Purchase Report";
            this.purchaseReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReportToolStripMenuItem_Click);
            // 
            // voucherReportToolStripMenuItem
            // 
            this.voucherReportToolStripMenuItem.Name = "voucherReportToolStripMenuItem";
            this.voucherReportToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.voucherReportToolStripMenuItem.Text = "Voucher Report";
            this.voucherReportToolStripMenuItem.Click += new System.EventHandler(this.voucherReportToolStripMenuItem_Click);
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationParameterToolStripMenuItem1,
            this.applicationParameterToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(74, 27);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // applicationParameterToolStripMenuItem1
            // 
            this.applicationParameterToolStripMenuItem1.Name = "applicationParameterToolStripMenuItem1";
            this.applicationParameterToolStripMenuItem1.Size = new System.Drawing.Size(226, 28);
            this.applicationParameterToolStripMenuItem1.Text = "User Master";
            this.applicationParameterToolStripMenuItem1.Click += new System.EventHandler(this.UserMasterToolStripMenuItem1_Click);
            // 
            // applicationParameterToolStripMenuItem
            // 
            this.applicationParameterToolStripMenuItem.Name = "applicationParameterToolStripMenuItem";
            this.applicationParameterToolStripMenuItem.Size = new System.Drawing.Size(226, 28);
            this.applicationParameterToolStripMenuItem.Text = "Application Parameter";
            this.applicationParameterToolStripMenuItem.Click += new System.EventHandler(this.ApplicationParameterToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(226, 28);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.LogOutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripFinYear,
            this.toolStripLoginUser});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(949, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStripFinYear
            // 
            this.toolStripFinYear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripFinYear.Name = "toolStripFinYear";
            this.toolStripFinYear.Size = new System.Drawing.Size(118, 17);
            this.toolStripFinYear.Text = "toolStripStatusLabel1";
            // 
            // toolStripLoginUser
            // 
            this.toolStripLoginUser.BackColor = System.Drawing.Color.PeachPuff;
            this.toolStripLoginUser.Name = "toolStripLoginUser";
            this.toolStripLoginUser.Size = new System.Drawing.Size(118, 17);
            this.toolStripLoginUser.Text = "toolStripStatusLabel1";
            // 
            // productionReportToolStripMenuItem
            // 
            this.productionReportToolStripMenuItem.Name = "productionReportToolStripMenuItem";
            this.productionReportToolStripMenuItem.Size = new System.Drawing.Size(202, 28);
            this.productionReportToolStripMenuItem.Text = "Production Report";
            this.productionReportToolStripMenuItem.Click += new System.EventHandler(this.productionReportToolStripMenuItem_Click);
            // 
            // MasterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MasterPage";
            this.Text = "D World";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterPage_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem mastersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem designationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripFinYear;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoginUser;
        private System.Windows.Forms.ToolStripMenuItem purchaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voucherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationParameterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockRegisterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voucherReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionReportToolStripMenuItem;
    }
}



