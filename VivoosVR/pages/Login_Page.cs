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
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;

namespace VivoosVR
{
    public partial class Login_Page : MasterForm
    {
        public Login_Page()
        {
            //Page_Load();
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
        }
        protected void Page_Load()
        {
            int flag = 0;
            string connectionString = "Data Source=.\\SQLEXPRESS; Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr[0].ToString() == "Vivos")
                            {
                                flag = 1;
                                con.Close();
                                break;
                            }
                        }
                    }
                }

                if (flag == 0)
                {
                    string startuppath = System.IO.Path.GetFullPath(".\\");
                    string MDFCreator = startuppath + "\\VivoosMDFCreator.sql";
                    string TableCreator = startuppath + "\\VivoosTableCreator.sql";
                    string elementAdder = startuppath + "\\ElementAdder.sql";
                    try
                    {
                        string sqlConnectionString = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        //string script = File.ReadAllText(@"D:\GitHub\VivoosVR\UserInterface\LocalVersion\VivoosVR\VivoosVR\VivoosMDFCreator.sql");
                        string script = File.ReadAllText(MDFCreator);
                        SqlConnection conn = new SqlConnection(sqlConnectionString);
                        //SqlConnection conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        Server server = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString1 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        //string script1 = File.ReadAllText(@"D:\GitHub\VivoosVR\UserInterface\LocalVersion\VivoosVR\VivoosVR\VivoosTableCreator.sql");
                        string script1 = File.ReadAllText(TableCreator);
                        SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
                        //SqlConnection conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        Server server1 = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString2 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        //string script = File.ReadAllText(@"D:\GitHub\VivoosVR\UserInterface\LocalVersion\VivoosVR\VivoosVR\VivoosTableCreator.sql");
                        string script2 = File.ReadAllText(elementAdder);
                        SqlConnection conn2 = new SqlConnection(sqlConnectionString2);
                        //SqlConnection conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        Server server2 = new Server(new ServerConnection(conn2));
                        server.ConnectionContext.ExecuteNonQuery(script2);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());

                        string sqlConnectionString = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        //string script = File.ReadAllText(@"D:\GitHub\VivoosVR\UserInterface\LocalVersion\VivoosVR\VivoosVR\VivoosTableCreator.sql");
                        string script = File.ReadAllText(TableCreator);
                        SqlConnection conn = new SqlConnection(sqlConnectionString);
                        //SqlConnection conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        Server server = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString1 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        //string script = File.ReadAllText(@"D:\GitHub\VivoosVR\UserInterface\LocalVersion\VivoosVR\VivoosVR\VivoosTableCreator.sql");
                        string script1 = File.ReadAllText(elementAdder);
                        SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
                        //SqlConnection conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        Server server1 = new Server(new ServerConnection(conn1));
                        server.ConnectionContext.ExecuteNonQuery(script1);
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Checks the case of all textboxes are filled
            try
            {
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
            catch (Exception)
            {
                /*SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                con.Open();
                string str = "USE Master;";
                //string str1 = "ALTER DATABASE Vivos SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                string str2 = "RESTORE DATABASE Vivos FROM DISK='.\\Vivos.bak' WITH REPLACE";
                SqlCommand cmd = new SqlCommand(str, con);
                //SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd.ExecuteNonQuery();
                // cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                con.Close();*/
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

        private void Login_Page_Load(object sender, EventArgs e)
        {
            /*int flag = 0;
            string connectionString = "Data Source=.\\SQLEXPRESS; Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr[0].ToString()=="Vivos")
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }
                }
                if (flag==0)
                {
                    string startupPath = System.IO.Path.GetFullPath(".\\");
                    string bak= startupPath + "\\Vivos.bak"; 
                    try
                    {
                        startupPath = System.IO.Path.GetFullPath(".\\");
                        string startupPath1 = System.IO.Directory.GetParent(@"../../../").FullName;
                        // System.IO.File.Copy(startupPath1 + "\\Database\\Vivos.bak", "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup", true);
                        //System.IO.File.Move(startupPath1 + "\\Database\\Vivos.bak", "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup");
                        SqlConnection con1 = new SqlConnection(@"Data Source=.\SQLEXPRESS" + ";" + ";Integrated Security=True");
                        con1.Open();
                        string str = "USE Master;";
                        string bak1 = startupPath1 + "\\Database\\Vivos.bak";
                        bak = startupPath + "\\Vivos.bak";
                        string str1 = "RESTORE DATABASE Vivos FROM DISK='.\\Vivos.bak' WITH REPLACE";
                        //string str1 = "RESTORE DATABASE Vivos FROM DISK='" + bak + "' WITH REPLACE";
                        //string str1 = "RESTORE DATABASE Vivos FROM DISK='.\\Vivos.bak' WITH REPLACE";
                        SqlCommand cmd = new SqlCommand(str, con1);
                        SqlCommand cmd1 = new SqlCommand(str1, con1);
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show(bak);
                    }
                }
            }*/
        }
    }
}