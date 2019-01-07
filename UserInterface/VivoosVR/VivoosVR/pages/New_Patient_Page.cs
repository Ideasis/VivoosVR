using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Diagnostics;
namespace VivoosVR
{
    public partial class New_Patient_Page : MasterForm
    {
        public New_Patient_Page()
        {
            InitializeComponent();
            edit_start();
            PlaceSelfOnSecondMonitor();
        }
        public void edit_start()
        {
            if (GlobalVariables.Is_Edit == true)
            {
                using (VivosEntities db = new VivosEntities())
                {
                    List<Patient> patientlist = (from x in db.Patients select x).ToList();
                    for (int i = 0; i < patientlist.Count; i++)
                    {
                        if(patientlist[i].Id==GlobalVariables.Edit_ID)
                        {
                            txtPatientCode.Text = patientlist[i].Code;
                            txtDescription.Text= patientlist[i].Notes;
                            if(patientlist[i].DateOfBirth!=null)
                            { 
                                string[] datetime = ((patientlist[i].DateOfBirth).ToString()).Split(' ');
                                clndrBirthday.Value =Convert.ToDateTime(datetime[0]);
                            }
                        }
                    }
                }
            }
        }
        private void new_patient_Load(object sender, EventArgs e)
        {
            lblPatientCode.Text = resourceManager.GetString("headerPatient", GlobalVariables.uiLanguage);
            lblBirthday.Text = resourceManager.GetString("headerBirthday", GlobalVariables.uiLanguage);
            lblNotes.Text = resourceManager.GetString("headerNotes", GlobalVariables.uiLanguage);
            btnLogout.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            btnCancel.Text = resourceManager.GetString("btnCancel1", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formNewPatient", GlobalVariables.uiLanguage);
        }

        private void new_patient_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult exit = new DialogResult();
            exit = MessageBox.Show(resourceManager.GetString("msgContinue", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                Login_Page login_page = new Login_Page();
                this.Hide();
                login_page.Show();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult exit = new DialogResult();
            exit = MessageBox.Show(resourceManager.GetString("msgContinue", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                GlobalVariables.Is_Edit = false;
                GlobalVariables.Patients_Search_Flag = 0;
                Patients_Page patients = new Patients_Page();
                this.Hide();
                patients.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Is_Edit == false)
            {
                int flag = 0;
                if (String.IsNullOrEmpty(txtPatientCode.Text))
                {
                    DialogResult error = new DialogResult();
                    error = MessageBox.Show(resourceManager.GetString("msgPatient", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (DateTime.Compare(clndrBirthday.Value, DateTime.Now) > 0)
                {
                    DialogResult error = new DialogResult();
                    error = MessageBox.Show(resourceManager.GetString("msgDate", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (VivosEntities db = new VivosEntities())
                    {
                        List<Patient> patientList = (from x in db.Patients select x).ToList();
                        Patient new_patient = new Patient();
                        for (int i = 0; i < patientList.Count; i++)
                        {
                            if (patientList[i].Code == txtPatientCode.Text)
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            new_patient.Code = txtPatientCode.Text.ToString();
                            new_patient.Id = Guid.NewGuid();
                            new_patient.DoctorId = GlobalVariables.LoginID;
                            new_patient.IsApproved = true;
                            new_patient.EntryDate = DateTime.Now;

                            string[] doğumTarihi = (clndrBirthday.Value.ToString()).Split(' ');
                            string[] sistemTarihi = (DateTime.Now.ToString()).Split(' ');
                            if (doğumTarihi[0] == sistemTarihi[0])
                                new_patient.DateOfBirth = null;
                            else
                                new_patient.DateOfBirth = clndrBirthday.Value;
                            new_patient.Notes = txtDescription.Text.ToString();
                            db.Patients.Add(new_patient);
                            db.SaveChanges();
                            DialogResult information = new DialogResult();
                            information = MessageBox.Show(resourceManager.GetString("msgPatientAdded", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (flag == 1)
                        {
                            MessageBox.Show(resourceManager.GetString("msgUserExists", GlobalVariables.uiLanguage));
                        }
                       
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtPatientCode.Text))
                {
                    DialogResult error = new DialogResult();
                    error = MessageBox.Show(resourceManager.GetString("msgPatient", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (DateTime.Compare(clndrBirthday.Value, DateTime.Now) > 0)
                {
                    DialogResult error = new DialogResult();
                    error = MessageBox.Show(resourceManager.GetString("msgDate", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int flag1 = 0;
                    using (VivosEntities db = new VivosEntities())
                    {
                        Patient patient = (from x in db.Patients where x.Id == GlobalVariables.Edit_ID select x).SingleOrDefault();
                        if (patient != null)
                        {
                            if (patient.Code == txtPatientCode.Text)
                            {
                                string[] doğumTarihi = (clndrBirthday.Value.ToString()).Split(' ');
                                string[] sistemTarihi = (DateTime.Now.ToString()).Split(' ');
                                if (doğumTarihi[0] == sistemTarihi[0])
                                    patient.DateOfBirth = null;
                                else
                                    patient.DateOfBirth = clndrBirthday.Value;
                                patient.Notes = txtDescription.Text.ToString();
                                db.SaveChanges();
                                DialogResult information = new DialogResult();
                                information = MessageBox.Show(resourceManager.GetString("msgPatientEdited", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                List<Patient> patientList = (from x in db.Patients select x).ToList();
                                for (int i = 0; i < patientList.Count; i++)
                                {
                                    if (patientList[i].Code == txtPatientCode.Text)
                                    {
                                        flag1 = 1;
                                        break;
                                    }
                                }
                                if (flag1 == 0)
                                {
                                    patient.Code = txtPatientCode.Text;
                                    string[] doğumTarihi = (clndrBirthday.Value.ToString()).Split(' ');
                                    string[] sistemTarihi = (DateTime.Now.ToString()).Split(' ');
                                    if (doğumTarihi[0] == sistemTarihi[0])
                                        patient.DateOfBirth = null;
                                    else
                                        patient.DateOfBirth = clndrBirthday.Value;
                                    patient.Notes = txtDescription.Text.ToString();
                                    db.SaveChanges();
                                    DialogResult information = new DialogResult();
                                    information = MessageBox.Show(resourceManager.GetString("msgPatientEdited", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (flag1 == 1)
                                {
                                    MessageBox.Show(resourceManager.GetString("msgUserExists", GlobalVariables.uiLanguage));
                                }
                            }
                            
                        }

                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GlobalVariables.Is_Edit = false;
            GlobalVariables.Patients_Search_Flag = 0;
            Patients_Page patients = new Patients_Page();
            this.Hide();
            patients.Show();
        }
    }
}
