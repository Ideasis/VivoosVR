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
    public partial class New_User_Page : MasterForm
    {
        public New_User_Page()
        {
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin_Page admin = new Admin_Page();
            this.Hide();
            admin.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrEmpty(txtNewPassword.Text) || String.IsNullOrEmpty(txtNewPasswordAgain.Text) )
            {
                DialogResult error = new DialogResult();
                error = MessageBox.Show(resourceManager.GetString("msgFields", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (VivosEntities db = new VivosEntities())
                {
                    int flag = 0;
                    List<Consumer> consumerList = (from x in db.Consumers select x).ToList();
                    Consumer new_consumer = new Consumer();
                    User new_user = new User();
                    for (int i = 0; i < consumerList.Count; i++)
                    {
                        if (consumerList[i].Email==txtUser.Text)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag==0)
                    {
                        if (txtNewPassword.Text==txtNewPasswordAgain.Text)
                        {
                            Guid consumer_id = Guid.NewGuid();
                            new_consumer.Id = consumer_id;
                            new_consumer.Code = Guid.NewGuid().ToString();
                            new_consumer.Description = txtNameSurname.Text;
                            new_consumer.Email = txtUser.Text;
                            new_consumer.EntryDate = DateTime.Now;

                            Consumer consumer = (from x in db.Consumers where x.Code == "admin" select x).SingleOrDefault();
                            User user = (from x in db.Users where x.Id == consumer.Id select x).SingleOrDefault();

                            new_user.Id = consumer_id;
                            new_user.GroupId = user.GroupId;
                            new_user.Password = txtNewPassword.Text;
                            db.Consumers.Add(new_consumer);
                            db.Users.Add(new_user);
                            db.SaveChanges();
                            DialogResult information = new DialogResult();
                            information = MessageBox.Show(resourceManager.GetString("msgUserAdded", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(resourceManager.GetString("msgNewPasswordTwice", GlobalVariables.uiLanguage));
                        }
                        
                    }
                    else if (flag==1)
                    {
                        MessageBox.Show(resourceManager.GetString("msgUserExists", GlobalVariables.uiLanguage));
                    } 
                }
            }
        }
        private void new_user_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }
        private void New_User_Page_Load(object sender, EventArgs e)
        {
            lblUsername.Text = resourceManager.GetString("lblUsername", GlobalVariables.uiLanguage);
            lblNameSurname.Text = resourceManager.GetString("lblNameSurname", GlobalVariables.uiLanguage);
            lblNewPassword.Text = resourceManager.GetString("lblNewPassword", GlobalVariables.uiLanguage);
            lblNewPasswordAgain.Text = resourceManager.GetString("lblNewPasswordAgain", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formNewUser", GlobalVariables.uiLanguage);
        }
    }
}
