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
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Diagnostics;
namespace VivoosVR
{
    public partial class Patients_Page : MasterForm
    {
        public Patients_Page()
        {
            string key=null;
            InitializeComponent();
            fill_datagrid(key);
            PlaceSelfOnSecondMonitor();
        }
        public void fill_datagrid(string key)
        {
            using (VivosEntities db = new VivosEntities())
            {
                patients_datagrid.Rows.Clear();
                patients_datagrid.ColumnCount = 4;
                patients_datagrid.Columns[0].Visible = false;
                patients_datagrid.Columns[1].Name = resourceManager.GetString("headerPatient", GlobalVariables.uiLanguage);
                patients_datagrid.Columns[2].Name = resourceManager.GetString("headerBirthday", GlobalVariables.uiLanguage);
                patients_datagrid.Columns[3].Name = resourceManager.GetString("headerRegistration", GlobalVariables.uiLanguage); 
                patients_datagrid.RowTemplate.Height = 30;

                patients_datagrid.BorderStyle = BorderStyle.None;
                patients_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                patients_datagrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                patients_datagrid.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                patients_datagrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                patients_datagrid.BackgroundColor = Color.White;

                patients_datagrid.EnableHeadersVisualStyles = false;
                patients_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                patients_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                patients_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                add_button();
                for (int i = 0; i < patients_datagrid.ColumnCount-3; i++)
                {
                    patients_datagrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    patients_datagrid.Columns[i].DefaultCellStyle.Font = new Font("Times New Roman",12,FontStyle.Regular);
                }
                List<Patient> patientlist = (from x in db.Patients where x.DoctorId == GlobalVariables.LoginID select x).ToList();
                List<Patient> patientlist1 = patientlist.OrderBy(o => o.Code).ToList();
                List<Patient> patientlist02 = (from x in db.Patients where x.Code.StartsWith(key) && x.DoctorId == GlobalVariables.LoginID select x).ToList();
                List<Patient> patientlist2 = patientlist02.OrderBy(o => o.Code).ToList();
                List<Patient> keyList = null;
                if (GlobalVariables.Patients_Search_Flag==0)
                    keyList = patientlist1;
                else       
                    keyList = patientlist2;
                for (int i = 0; i < keyList.Count; i++)
                {
                    i = patients_datagrid.Rows.Add();
                    patients_datagrid.Rows[i].Cells[0].Value = keyList[i].Id;
                    patients_datagrid.Rows[i].Cells[1].Value = keyList[i].Code;
                    if (keyList[i].DateOfBirth != null)
                    {
                        patients_datagrid.Rows[i].Cells[2].Value = Convert.ToDateTime(keyList[i].DateOfBirth).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); 
                    }
                    else
                        patients_datagrid.Rows[i].Cells[2].Value = null;
                    if (keyList[i].EntryDate != null)
                    {
                        patients_datagrid.Rows[i].Cells[3].Value = Convert.ToDateTime(keyList[i].EntryDate).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                        patients_datagrid.Rows[i].Cells[3].Value = null;               
                }
            }
        }
        public void add_button()
        {
            DataGridViewLinkColumn lnkNotes= new DataGridViewLinkColumn();
            patients_datagrid.Columns.Add(lnkNotes);
            lnkNotes.HeaderText = resourceManager.GetString("headerNotes", GlobalVariables.uiLanguage); 
            lnkNotes.UseColumnTextForLinkValue = true;
            lnkNotes.Text = resourceManager.GetString("linkNotes", GlobalVariables.uiLanguage);
            lnkNotes.ActiveLinkColor = Color.White;
            lnkNotes.LinkBehavior = LinkBehavior.SystemDefault;
            lnkNotes.LinkColor = Color.Blue;
            lnkNotes.TrackVisitedState = true;
            lnkNotes.VisitedLinkColor = Color.YellowGreen;
            lnkNotes.Width = 150;

            DataGridViewButtonColumn btnSessions = new DataGridViewButtonColumn();
            patients_datagrid.Columns.Add(btnSessions);
            btnSessions.HeaderText = resourceManager.GetString("headerSession", GlobalVariables.uiLanguage);
            btnSessions.Text = resourceManager.GetString("headerSession", GlobalVariables.uiLanguage);
            btnSessions.Name = "sessionBtn";
            btnSessions.UseColumnTextForButtonValue = true;
            btnSessions.Width = 150;

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            patients_datagrid.Columns.Add(btnEdit);
            btnEdit.HeaderText = resourceManager.GetString("headerEdit", GlobalVariables.uiLanguage);
            btnEdit.Text = resourceManager.GetString("headerEdit", GlobalVariables.uiLanguage);
            btnEdit.Name = "gridBtn";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.Width = 150;

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            patients_datagrid.Columns.Add(btnDelete);
            btnDelete.HeaderText = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Text = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Name = "deleteBtn";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 150;
        }
       
        public void find_notes (Guid id)
        {
            using (VivosEntities db = new VivosEntities())
            {
                List<Patient> patientlist = (from x in db.Patients select x).ToList();
                for (int i = 0; i < patientlist.Count; i++)
                {
                    if(patientlist[i].Id == id)
                    {
                        MessageBox.Show(patientlist[i].Notes, patientlist[i].Code + " " + resourceManager.GetString("headerNotes", GlobalVariables.uiLanguage));  
                    }
                }
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
                GlobalVariables.Patients_Search_Flag = 1;
                fill_datagrid(txtSearch.Text.ToString());
        }
        private void patients_Load(object sender, EventArgs e)
        {
            lblSearch.Text = resourceManager.GetString("lblSearch", GlobalVariables.uiLanguage);
            btnLogout.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            btnAddNewPatient.Text = resourceManager.GetString("btnNewPatient", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formPatient", GlobalVariables.uiLanguage);
        }
        private void patients_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void btnAddNewPatient_Click(object sender, EventArgs e)
        {
            GlobalVariables.Is_Edit = false;
            New_Patient_Page yeni_danısan = new New_Patient_Page();
            this.Hide();
            yeni_danısan.Show();
        }

        private void patients_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    Guid id = Guid.Parse(patients_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    find_notes(id);
                }
                catch (Exception)
                {

                    
                }
            }
            else if (e.ColumnIndex == 5)
            {
                try
                {
                    GlobalVariables.Session_ID = Guid.Parse(patients_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    GlobalVariables.Is_Session = true;
                    using (VivosEntities db = new VivosEntities())
                    {
                        List<Patient> patientlist1 = (from x in db.Patients where x.Id == GlobalVariables.Session_ID select x).ToList();
                        GlobalVariables.Session_ID_name = patientlist1[0].Code;
                    }
                    GlobalVariables.Sessions_Search_Flag = 0;
                    Sessions_Page sessions = new Sessions_Page();
                    this.Hide();
                    sessions.Show();
                }
                catch (Exception)
                {

                    
                }
            }
            else if (e.ColumnIndex == 6)
            {
                try
                {
                    GlobalVariables.Edit_ID = Guid.Parse(patients_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    GlobalVariables.Is_Edit = true;
                    New_Patient_Page yeni_danısan = new New_Patient_Page();
                    this.Hide();
                    yeni_danısan.Show();
                }
                catch (Exception)
                {

                    
                }
            }
            else if (e.ColumnIndex == 7)
            {
                try
                {
                    DialogResult delete = new DialogResult();
                    delete = MessageBox.Show(resourceManager.GetString("msgIsPatientDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (delete == DialogResult.Yes)
                    {
                        GlobalVariables.Edit_ID = Guid.Parse(patients_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                        using (VivosEntities db = new VivosEntities())
                        {
                            List<Session> sessionList = (from x in db.Sessions where x.PatientId == GlobalVariables.Edit_ID select x).ToList();
                            Session[] sessions = new Session[sessionList.Count];

                            for (int i = 0; i < sessions.Length; i++)
                            {
                                sessions[i] = sessionList[i];
                                db.Sessions.Remove(sessions[i]);
                            }
                            Patient patient = db.Patients.First(x => x.Id == GlobalVariables.Edit_ID);

                            db.Patients.Remove(patient);
                            db.SaveChanges();
                            GlobalVariables.Patients_Search_Flag = 0;
                            string key = null;
                            fill_datagrid(key);
                            DialogResult information = new DialogResult();
                            information = MessageBox.Show(resourceManager.GetString("msgPatientDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception)
                {


                }

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

        private void patients_datagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*DataGridViewRow row = patients_datagrid.Rows[e.RowIndex];
            row.DefaultCellStyle.BackColor = Color.Green;*/
        }
    }
}
