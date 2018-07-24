using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace VivoosVR
{
    public partial class Login_Page : MasterForm
    {
        public Login_Page()
        {
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Checks the case of all textboxes are filled
            if (String.IsNullOrEmpty(txtUsername.Text) || String.IsNullOrEmpty(txtPassword.Text))
            {
                DialogResult error = new DialogResult();
                error = MessageBox.Show(resourceManager.GetString("msgFields", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                using (VivosEntities db = new VivosEntities())
                {
                    Consumer consumer = (Consumer)(from x in db.Consumers where x.Email.Equals(txtUsername.Text) select x).FirstOrDefault();

                    if (consumer != null)
                    {
                        User user = (User)(from x in db.Users where consumer.Id.Equals(x.Id) && x.Password.Equals(txtPassword.Text) select x).FirstOrDefault();

                        if (user != null)
                        {
                            GlobalVariables.LoginID = consumer.Id;

                            if (consumer.Code.Equals("admin"))
                            {
                                Admin_Page admin = new Admin_Page();
                                this.Hide();
                                admin.Show();
                            }

                            else
                            {
                                Patients_Page patients = new Patients_Page();
                                this.Hide();
                                patients.Show();
                            }
                            return;
                        }
                    }
                }

                DialogResult error = new DialogResult();
                error = MessageBox.Show(resourceManager.GetString("msgWrong", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult exit = new DialogResult();
            exit = MessageBox.Show(resourceManager.GetString("msgContinue", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void getres(CultureInfo ci)
        {
            lblUsername.Text = resourceManager.GetString("lblUsername", ci);
            lblPassword.Text = resourceManager.GetString("lblPassword", ci);
            btnCancel.Text = resourceManager.GetString("btnCancel", ci);
            btnLogin.Text = resourceManager.GetString("btnLogin", ci);
            btnEnglish.Text = resourceManager.GetString("btnEnglish", GlobalVariables.uiLanguage);
            btnTurkish.Text = resourceManager.GetString("btnTurkish", GlobalVariables.uiLanguage);
            btnArabic.Text = resourceManager.GetString("btnArabic", GlobalVariables.uiLanguage);
            btnChangePassword.Text = resourceManager.GetString("btnChangePassword", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formLogin", ci);
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            GlobalVariables.uiLanguage = new CultureInfo("en-US");
            getres(GlobalVariables.uiLanguage);
        }

        private void btnTurkish_Click(object sender, EventArgs e)
        {
            GlobalVariables.uiLanguage = new CultureInfo("tr-TR");
            getres(GlobalVariables.uiLanguage);
        }

        private void btnArabic_Click(object sender, EventArgs e)
        {
            GlobalVariables.uiLanguage = new CultureInfo("ar-SA");
            getres(GlobalVariables.uiLanguage);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            Change_Password_Page change_password = new Change_Password_Page();
            this.Hide();
            change_password.Show();
        }
    }
}