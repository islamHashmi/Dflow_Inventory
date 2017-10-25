using System;
using System.Linq;
using System.Windows.Forms;
using Dflow_Inventory.DataContext;

namespace Dflow_Inventory.ContentPage
{
    public partial class ApplicationParameter : Form
    {
        private Inventory_DflowEntities db;

        public ApplicationParameter()
        {
            InitializeComponent();
        }

        private void ApplicationParameter_Load(object sender, EventArgs e)
        {
            try
            {
                using (db = new Inventory_DflowEntities())
                {
                    Clear_Fields();

                    var app = db.Application_Parameter.FirstOrDefault();

                    if(app != null)
                    {
                        TxtCompanyName.Text = app.companyName;
                        TxtDealsIn.Text = app.dealsIn;
                        TxtAddress1.Text = app.addressLine1;
                        TxtAddress2.Text = app.addressLine2;
                        TxtMobileNo1.Text = app.mobileNo1;
                        TxtMobileNo2.Text = app.mobileNo2;
                        TxtEmailId1.Text = app.emailAddress1;
                        TxtEmailId2.Text = app.emailAddress2;
                        TxtPanNo.Text = app.panNumber;
                        TxtHsnCode.Text = app.hsnCode;
                        TxtGstNo.Text = app.gstNumber;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Clear_Fields()
        {
            TxtCompanyName.Text = string.Empty;
            TxtDealsIn.Text = string.Empty;
            TxtAddress1.Text = string.Empty;
            TxtAddress2.Text = string.Empty;
            TxtMobileNo1.Text = string.Empty;
            TxtMobileNo2.Text = string.Empty;
            TxtEmailId1.Text = string.Empty;
            TxtEmailId2.Text = string.Empty;
            TxtPanNo.Text = string.Empty;
            TxtHsnCode.Text = string.Empty;
            TxtGstNo.Text = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Parameter app = new Application_Parameter();

                using (db = new Inventory_DflowEntities())
                {
                    app = db.Application_Parameter.FirstOrDefault();

                    if (app == null)
                    {
                        app = new Application_Parameter();
                        db.Application_Parameter.Add(app);
                    }

                    app.companyName = string.IsNullOrEmpty(TxtCompanyName.Text) ? null : TxtCompanyName.Text;
                    app.dealsIn = string.IsNullOrEmpty(TxtDealsIn.Text) ? null : TxtDealsIn.Text;
                    app.addressLine1 = string.IsNullOrEmpty(TxtAddress1.Text) ? null : TxtAddress1.Text;
                    app.addressLine2 = string.IsNullOrEmpty(TxtAddress2.Text) ? null : TxtAddress2.Text;
                    app.mobileNo1 = string.IsNullOrEmpty(TxtMobileNo1.Text) ? null : TxtMobileNo1.Text;
                    app.mobileNo2 = string.IsNullOrEmpty(TxtMobileNo2.Text) ? null : TxtMobileNo2.Text;
                    app.emailAddress1 = string.IsNullOrEmpty(TxtEmailId1.Text) ? null : TxtEmailId1.Text;
                    app.emailAddress2 = string.IsNullOrEmpty(TxtEmailId2.Text) ? null : TxtEmailId2.Text;
                    app.panNumber = string.IsNullOrEmpty(TxtPanNo.Text) ? null : TxtPanNo.Text;
                    app.hsnCode = string.IsNullOrEmpty(TxtHsnCode.Text) ? null : TxtHsnCode.Text;
                    app.gstNumber = string.IsNullOrEmpty(TxtGstNo.Text) ? null : TxtGstNo.Text;

                    db.SaveChanges();

                    MessageBox.Show("Data Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
