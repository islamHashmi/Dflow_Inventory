﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;

namespace Dflow_Inventory.ContentPage
{
    public partial class ChangePassword : Form
    {
        private Inventory_DflowEntities db;

        private int _userId;
        private string _loginId;

        public ChangePassword(int userId, string loginId)
        {
            InitializeComponent();

            _userId = userId;
            _loginId = loginId;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(TxtOldPassword.Text))
                {
                    MessageBox.Show("Old Password is mandatory.");
                    return;
                }

                if (string.IsNullOrEmpty(TxtNewPassword.Text))
                {
                    MessageBox.Show("New Password is mandatory.");
                    return;
                }

                if (TxtNewPassword.Text != TxtConfirmPass.Text)
                {
                    MessageBox.Show("Both Passwords do not match.");
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    var user = db.User_Master.FirstOrDefault(m => m.userId == _userId && m.loginId == _loginId);

                    if(user == null)
                    {
                        MessageBox.Show("User does not exists.");
                        return;
                    }

                    user.loginKey = TxtNewPassword.Text.Trim();
                    user.updatedBy = SessionHelper.UserId;
                    user.updatedOn = DateTime.Now;

                    db.SaveChanges();

                    MessageBox.Show("Password Updated Successfully.");

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            TxtLoginId.Text = _loginId;
        }
    }
}
