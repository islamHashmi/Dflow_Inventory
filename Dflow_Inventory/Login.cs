﻿using System;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System.Data.Entity.Validation;
using System.Drawing;

namespace Dflow_Inventory
{
    public partial class Login : Form
    {
        private Inventory_DflowEntities db;

        public Login()
        {
            InitializeComponent();            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtLoginId.Text))
                {
                    MessageBox.Show("Login Id is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TxtLoginId.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    MessageBox.Show("Password is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TxtPassword.Focus();
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    var login = db.UserMasters.FirstOrDefault(m => m.loginId == TxtLoginId.Text.Trim() && m.loginKey == TxtPassword.Text.Trim());
                    

                    if (login == null)
                    {
                        MessageBox.Show("Invalid Login Id or Password !!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    SessionHelper.UserId = login.userId;
                    SessionHelper.UserName = login.name;

                    login.lastlogin = DateTime.Now;

                    db.SaveChanges();

                    //MessageBox.Show("Logged In Successfully...");

                    if (db.ApplicationParameters.FirstOrDefault() != null)
                    {
                        SessionHelper.HsnCode = db.ApplicationParameters.FirstOrDefault().hsnCode;
                    }

                    string finYear = Get_FinancialYear();

                    SessionHelper.FinYear = finYear;                    

                    MasterPage mdi = new MasterPage();                    
                    mdi.WindowState = FormWindowState.Maximized;
                    mdi.Show();

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message);
            }
        }

        private string Get_FinancialYear()
        {
            string _finYear = string.Empty, _shortFinYear = string.Empty;

            DateTime currentDate = DateTime.Today;

            using (db = new Inventory_DflowEntities())
            {
                var finYear = db.FinancialYears.FirstOrDefault(m => m.startDate <= currentDate && m.endDate >= currentDate);

                if (finYear != null)
                {
                    _shortFinYear = finYear.finYear;
                }
                else
                {
                    FinancialYear fy = new FinancialYear();

                    int CurrentYear = DateTime.Today.Year;
                    int PreviousYear = DateTime.Today.Year - 1;
                    int NextYear = DateTime.Today.Year + 1;

                    if (DateTime.Today.Month > 3)
                    {
                        _finYear = string.Format("01/04/{0}-31/03/{1}", CurrentYear, NextYear);

                        _shortFinYear = string.Format("{0}{1}", CurrentYear.ToString().Substring(2, 2), NextYear.ToString().Substring(2, 2));

                        fy.startDate = (DateTime)CommanMethods.ConvertDate("01/04/" + CurrentYear);
                        fy.endDate = (DateTime)CommanMethods.ConvertDate("31/03/" + NextYear);
                    }
                    else
                    {
                        _finYear = string.Format("01/04/{0}-31/03/{1}", PreviousYear, CurrentYear);

                        _shortFinYear = string.Format("{0}{1}", CurrentYear.ToString().Substring(2, 2), NextYear.ToString().Substring(2, 2));

                        fy.startDate = (DateTime)CommanMethods.ConvertDate("01/04/" + PreviousYear);
                        fy.endDate = (DateTime)CommanMethods.ConvertDate("31/03/" + CurrentYear);
                    }

                    fy.finYear = _shortFinYear == string.Empty ? null : _shortFinYear;
                    fy.finYearDescription = _finYear == string.Empty ? null : _finYear;
                    fy.entryBy = SessionHelper.UserId;
                    fy.entryDate = DateTime.Now;

                    db.FinancialYears.Add(fy);
                    db.SaveChanges();                    
                }

                return _shortFinYear;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.FromArgb(63, 162, 204), ButtonBorderStyle.Solid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.FromArgb(63, 162, 204), ButtonBorderStyle.Solid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
