using System;
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

        public int UserId { get => _userId; set => _userId = value; }
        public string LoginId { get => _loginId; set => _loginId = value; }

        public ChangePassword(int userId, string loginId)
        {
            InitializeComponent();

            UserId = userId;
            LoginId = loginId;
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
                    var user = db.UserMasters.FirstOrDefault(m => m.userId == UserId && m.loginId == LoginId);

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
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            TxtLoginId.Text = LoginId;
        }
    }
}
