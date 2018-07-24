using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace VivoosVR
{
    public partial class Change_Password_Page : MasterForm
    {
        public Change_Password_Page()
        {
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login_Page login = new Login_Page();
            this.Hide();
            login.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOldPassword.Text) || String.IsNullOrEmpty(txtUsername.Text) || String.IsNullOrEmpty(txtNewPassword.Text) || String.IsNullOrEmpty(txtNewPasswordAgain.Text))
            {
                DialogResult error = new DialogResult();
                error = MessageBox.Show(resourceManager.GetString("msgFields", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (VivosEntities db = new VivosEntities())
                {
                    Consumer consumer = (from x in db.Consumers where x.Email==txtUsername.Text select x).SingleOrDefault();
                    if (consumer != null)
                    {
                        User user = (from x in db.Users where x.Id == consumer.Id select x).SingleOrDefault();
                        if (user!=null)
                        {
                            if (user.Password==txtOldPassword.Text)
                            {
                                if (txtNewPassword.Text==txtNewPasswordAgain.Text)
                                {
                                    user.Password = txtNewPassword.Text;
                                    db.SaveChanges();
                                    MessageBox.Show(resourceManager.GetString("msgPasswordChanged", GlobalVariables.uiLanguage));
                                }
                                else
                                {
                                    MessageBox.Show(resourceManager.GetString("msgNewPasswordTwice", GlobalVariables.uiLanguage));
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show(resourceManager.GetString("msgOldPasswordWrong", GlobalVariables.uiLanguage));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(resourceManager.GetString("msgUserNotFound", GlobalVariables.uiLanguage));
                    }
                }
            }
        }

        private void change_password_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void Change_Password_Page_Load(object sender, EventArgs e)
        {
            lblUsername.Text = resourceManager.GetString("lblUsername", GlobalVariables.uiLanguage);
            lblOldPassword.Text = resourceManager.GetString("lblOldPassword", GlobalVariables.uiLanguage);
            lblNewPassword.Text = resourceManager.GetString("lblNewPassword", GlobalVariables.uiLanguage);
            lblNewPasswordAgain.Text = resourceManager.GetString("lblNewPasswordAgain", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("btnChangePassword", GlobalVariables.uiLanguage);
        }
    }
}
