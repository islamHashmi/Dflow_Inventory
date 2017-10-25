using Dflow_Inventory.DataContext;
using Dflow_Inventory.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dflow_Inventory.ContentPage
{
    public partial class EmployeeMaster : Form
    {
        private Inventory_DflowEntities db;

        private int _employeeId = 0;

        public int EmployeeId { get => _employeeId; set => _employeeId = value; }

        public EmployeeMaster()
        {
            InitializeComponent();

            Bind_Combo();
            Autogenerate_CustomerCode();
            Clear_Date(DtpBirthDate);
            Clear_Date(DtpHireDate);
            Get_Data();
        }

        private void Bind_Combo()
        {
            using (db = new Inventory_DflowEntities())
            {
                CmbSex.DataSource = db.Genders.ToList();
                CmbSex.ValueMember = "genderCode";
                CmbSex.DisplayMember = "genderDescription";

                CmbMaritalStatus.DataSource = db.marital_Status.ToList();
                CmbMaritalStatus.ValueMember = "maritalStatusCode";
                CmbMaritalStatus.DisplayMember = "maritalStatusDesc";

                CmbEmpStatus.DataSource = db.Employment_Status.ToList();
                CmbEmpStatus.ValueMember = "empStatusCode";
                CmbEmpStatus.DisplayMember = "empStatusDesc";

                db.Designations.Distinct().ToList().Insert(0, new Designation { designationId = 0, designationName = "--- Select Designation ---" });

                CmbDesignation.DataSource = db.Designations.Distinct().ToList();
                CmbDesignation.ValueMember = "designationId";
                CmbDesignation.DisplayMember = "designationName";
            }
        }

        private void Autogenerate_CustomerCode()
        {
            using (db = new Inventory_DflowEntities())
            {
                int.TryParse(db.Employees.Max(m => m.employeeCode), out int _employeeCode);

                TxtEmpCode.Text = $"{_employeeCode + 1}";
            }
        }

        private void TxtEmailId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtEmailId.Text))
                {
                    if (!(bool)CommanMethods.Validate_Email(TxtEmailId.Text.Trim()))
                    {
                        MessageBox.Show("Invalid Email Id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Clear_Date(DateTimePicker dtp)
        {
            dtp.CustomFormat = " ";
            dtp.Format = DateTimePickerFormat.Custom;
        }

        private void Clear_Controls()
        {
            EmployeeId = 0;

            Autogenerate_CustomerCode();

            TxtFirstName.Text = string.Empty;
            TxtMiddleName.Text = string.Empty;
            TxtLastName.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtCity.Text = string.Empty;
            TxtState.Text = string.Empty;
            TxtPincode.Text = string.Empty;
            TxtMobileNo.Text = string.Empty;
            TxtEmailId.Text = string.Empty;

            Clear_Date(DtpBirthDate);
            Clear_Date(DtpHireDate);

            CmbSex.SelectedIndex = 0;
            CmbDesignation.SelectedIndex = 0;
            CmbMaritalStatus.SelectedIndex = 0;
            CmbEmpStatus.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtFirstName.Text == string.Empty)
                {
                    MessageBox.Show("FIrst Name is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (TxtPincode.Text == string.Empty)
                {
                    MessageBox.Show("Pincode is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (db = new Inventory_DflowEntities())
                {
                    Employee emp = new Employee();

                    if (EmployeeId == 0)
                    {
                        db.Employees.Add(emp);

                        emp.employeeCode = TxtEmpCode.Text.Trim();
                        emp.entryBy = SessionHelper.UserId;
                        emp.entryDate = DateTime.Now;
                        emp.active = true;
                    }
                    else
                    {
                        emp = db.Employees.FirstOrDefault(m => m.employeeId == EmployeeId && m.active == true);

                        emp.updatedBy = SessionHelper.UserId;
                        emp.updatedDate = DateTime.Now;
                    }

                    decimal _salary = 0;

                    decimal.TryParse(TxtSalary.Text, out _salary);

                    emp.firstName = string.IsNullOrWhiteSpace(TxtFirstName.Text) ? null : TxtFirstName.Text.Trim();
                    emp.middleName = string.IsNullOrWhiteSpace(TxtMiddleName.Text) ? null : TxtMiddleName.Text.Trim();
                    emp.lastName = string.IsNullOrWhiteSpace(TxtLastName.Text) ? null : TxtLastName.Text.Trim();
                    emp.address = string.IsNullOrWhiteSpace(TxtAddress.Text) ? null : TxtAddress.Text.Trim();
                    emp.city = string.IsNullOrWhiteSpace(TxtCity.Text) ? null : TxtCity.Text.Trim();
                    emp.state = string.IsNullOrWhiteSpace(TxtState.Text) ? null : TxtState.Text.Trim();
                    emp.pincode = string.IsNullOrWhiteSpace(TxtPincode.Text) ? null : TxtPincode.Text.Trim();
                    emp.mobileNo = string.IsNullOrWhiteSpace(TxtMobileNo.Text) ? null : TxtMobileNo.Text.Trim();
                    emp.emailId = string.IsNullOrWhiteSpace(TxtEmailId.Text) ? null : TxtEmailId.Text.Trim();
                    emp.salary = _salary == 0 ? null : (decimal?)_salary;
                    emp.sex = Convert.ToString(CmbSex.SelectedValue);
                    emp.designationId = CmbDesignation.SelectedIndex <= 0 ? null : (int?)Convert.ToInt32(CmbDesignation.SelectedValue);
                    emp.maritalStatus = Convert.ToString(CmbMaritalStatus.SelectedValue);
                    emp.empStatus = Convert.ToString(CmbEmpStatus.SelectedValue);
                    emp.dateOfBirth = CommanMethods.ConvertDate(DtpBirthDate.Text);
                    emp.hireDate = CommanMethods.ConvertDate(DtpHireDate.Text);

                    db.SaveChanges();

                    Clear_Controls();

                    Get_Data();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Controls();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CellContentClick(int columnIndex, int rowIndex)
        {
            if (rowIndex >= 0)
            {
                int.TryParse(Convert.ToString(DgvList["employeeId", rowIndex].Value), out int employeeId);

                using (db = new Inventory_DflowEntities())
                {
                    var sm = db.Employees.FirstOrDefault(x => x.employeeId == employeeId);

                    if (sm != null)
                    {
                        EmployeeId = sm.employeeId;
                        TxtEmpCode.Text = sm.employeeCode;
                        TxtFirstName.Text = sm.firstName;
                        TxtMiddleName.Text = sm.middleName;
                        TxtLastName.Text = sm.lastName;
                        TxtAddress.Text = sm.address;
                        TxtCity.Text = sm.city;
                        TxtState.Text = sm.state;
                        TxtPincode.Text = sm.pincode;
                        TxtMobileNo.Text = sm.mobileNo;
                        TxtEmailId.Text = sm.emailId;
                        TxtSalary.Text = string.Format("{0}", sm.salary);
                        CmbSex.SelectedValue = sm.sex;
                        CmbDesignation.SelectedValue = sm.designationId == null ? 0 : sm.designationId;
                        CmbMaritalStatus.SelectedValue = sm.maritalStatus;
                        CmbEmpStatus.SelectedValue = sm.empStatus;
                        DtpBirthDate.Text = sm.dateOfBirth == null ? "" : Convert.ToDateTime(sm.dateOfBirth).ToString("dd/MM/yyyy");
                        DtpHireDate.Text = sm.hireDate == null ? "" : Convert.ToDateTime(sm.hireDate).ToString("dd/MM/yyyy");
                    }
                }

                tabControl1.SelectedIndex = 0;
            }
        }

        private void DgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CellContentClick(e.ColumnIndex, e.RowIndex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DgvList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DgvList.CurrentCell != null)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        CellContentClick(DgvList.CurrentCell.ColumnIndex, DgvList.CurrentCell.RowIndex);
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        int.TryParse(s: Convert.ToString(DgvList["employeeId", DgvList.CurrentCell.RowIndex]), result: out int employeeId);

                        using (db = new Inventory_DflowEntities())
                        {
                            if (db.Employees.FirstOrDefault(x => x.employeeId == employeeId) != null)
                            {
                                db.Employees.FirstOrDefault(x => x.employeeId == employeeId).active = false;

                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Get_Data()
        {
            using (db = new Inventory_DflowEntities())
            {
                DgvList.DataSource = (from e in db.Employees
                                      join d in db.Designations on e.designationId equals d.designationId into des
                                      from d in des.DefaultIfEmpty()
                                      join g in db.Genders on e.sex equals g.genderCode into gm
                                      from g in gm.DefaultIfEmpty()
                                      join m in db.marital_Status on e.maritalStatus equals m.maritalStatusCode into ms
                                      from m in ms.DefaultIfEmpty()
                                      join em in db.Employment_Status on e.empStatus equals em.empStatusCode into ems
                                      from em in ems.DefaultIfEmpty()
                                      where e.active == true
                                      select new
                                      {
                                          employeeId = e.employeeId,
                                          employeeCode = e.employeeCode,
                                          firstName = e.firstName,
                                          middleName = e.middleName,
                                          lastName = e.lastName,
                                          address = e.address,
                                          city = e.city,
                                          state = e.state,
                                          pincode = e.pincode,
                                          mobileNo = e.mobileNo,
                                          emailId = e.emailId,
                                          salary = e.salary,
                                          sex = g.genderDescription,
                                          designation = d.designationName,
                                          maritalStatus = m.maritalStatusDesc,
                                          empStatus = em.empStatusDesc,
                                          dateOfBirth = e.dateOfBirth,
                                          hireDate = e.hireDate,
                                          active = e.active
                                      }).ToList();

                Set_Column_Employee();
            }
        }

        private void Set_Column_Employee()
        {
            DgvList.Columns["employeeId"].Visible = false;
            DgvList.Columns["active"].Visible = false;

            DgvList.Columns["employeeCode"].HeaderText = "Emp Code";
            DgvList.Columns["employeeCode"].DisplayIndex = 0;

            DgvList.Columns["firstName"].HeaderText = "First Name";
            DgvList.Columns["firstName"].DisplayIndex = 1;

            DgvList.Columns["middleName"].HeaderText = "Middle Name";
            DgvList.Columns["middleName"].DisplayIndex = 2;

            DgvList.Columns["lastName"].HeaderText = "Last Name";
            DgvList.Columns["lastName"].DisplayIndex = 3;

            DgvList.Columns["dateOfBirth"].HeaderText = "Date of Birth";
            DgvList.Columns["dateOfBirth"].DisplayIndex = 4;

            DgvList.Columns["hireDate"].HeaderText = "Hire Date";
            DgvList.Columns["hireDate"].DisplayIndex = 5;

            DgvList.Columns["address"].HeaderText = "Address";
            DgvList.Columns["address"].DisplayIndex = 6;

            DgvList.Columns["city"].HeaderText = "City";
            DgvList.Columns["city"].DisplayIndex = 7;

            DgvList.Columns["state"].HeaderText = "State";
            DgvList.Columns["state"].DisplayIndex = 8;

            DgvList.Columns["pincode"].HeaderText = "Pincode";
            DgvList.Columns["pincode"].DisplayIndex = 9;

            DgvList.Columns["mobileNo"].HeaderText = "Mobile No.";
            DgvList.Columns["mobileNo"].DisplayIndex = 10;

            DgvList.Columns["emailId"].HeaderText = "Email Id";
            DgvList.Columns["emailId"].DisplayIndex = 11;
            
            DgvList.Columns["salary"].HeaderText = "Salary";
            DgvList.Columns["salary"].DisplayIndex = 12;

            DgvList.Columns["sex"].HeaderText = "Sex";
            DgvList.Columns["sex"].DisplayIndex = 13;

            DgvList.Columns["designation"].HeaderText = "Designation";
            DgvList.Columns["designation"].DisplayIndex = 14;

            DgvList.Columns["maritalStatus"].HeaderText = "Marital";
            DgvList.Columns["maritalStatus"].DisplayIndex = 15;

            DgvList.Columns["empStatus"].HeaderText = "Emp Status";
            DgvList.Columns["empStatus"].DisplayIndex = 16;            

        }

        private void DgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,

                LineAlignment = StringAlignment.Center
            };

            Size textSize = TextRenderer.MeasureText(rowIdx, this.Font);

            if (grid.RowHeadersWidth < textSize.Width + 40)
            {
                grid.RowHeadersWidth = textSize.Width + 40;
            }

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void DtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            DtpBirthDate.CustomFormat = "dd/MM/yyyy";
        }

        private void DtpHireDate_ValueChanged(object sender, EventArgs e)
        {
            DtpHireDate.CustomFormat = "dd/MM/yyyy";
        }

        private void TxtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
