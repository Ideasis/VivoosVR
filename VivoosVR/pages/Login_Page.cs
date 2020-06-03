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
using System.Drawing;

namespace VivoosVR
{
    public partial class Login_Page : MasterForm
    {
        public Login_Page()
        {
            //Page_Load();
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
            getres(GlobalVariables.uiLanguage);

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
                        string script = File.ReadAllText(MDFCreator);
                        SqlConnection conn = new SqlConnection(sqlConnectionString);
                        Server server = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString1 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        string script1 = File.ReadAllText(TableCreator);
                        SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
                        Server server1 = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString2 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        string script2 = File.ReadAllText(elementAdder);
                        SqlConnection conn2 = new SqlConnection(sqlConnectionString2);
                        Server server2 = new Server(new ServerConnection(conn2));
                        server.ConnectionContext.ExecuteNonQuery(script2);

                        try
                        {
                            add_scenarios(Properties.Resources.Fear_Of_Dog_Park_File.ToString(), Properties.Resources.Fear_Of_Dog_Park_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Height_Balcony_File.ToString(), Properties.Resources.Fear_Of_Height_Balcony_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Spider_File.ToString(), Properties.Resources.Fear_Of_Spider_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Dark_File.ToString(), Properties.Resources.Fear_Of_Dark_Image);
                            add_scenarios(Properties.Resources.Social_Anxiety_Classroom_File.ToString(), Properties.Resources.Social_Anxiety_Classroom_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Height_Elevator_File.ToString(), Properties.Resources.Fear_Of_Height_Elevator_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Flight_File.ToString(), Properties.Resources.Fear_Of_Flight_Image);
                            add_scenarios(Properties.Resources.Park_Ayda_File.ToString(), Properties.Resources.Park_Ayda_Image);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.ToString());
                        }
                        
                    }
                    catch (Exception)
                    {
                        string sqlConnectionString = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        string script = File.ReadAllText(TableCreator);
                        SqlConnection conn = new SqlConnection(sqlConnectionString);
                        Server server = new Server(new ServerConnection(conn));
                        server.ConnectionContext.ExecuteNonQuery(script);

                        string sqlConnectionString1 = @"Integrated Security=SSPI;Persist Security Info=False;Data Source=.\SQLEXPRESS";
                        string script1 = File.ReadAllText(elementAdder);
                        SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
                        Server server1 = new Server(new ServerConnection(conn1));
                        server.ConnectionContext.ExecuteNonQuery(script1);


                        try
                        {
                            add_scenarios(Properties.Resources.Fear_Of_Dog_Park_File.ToString(), Properties.Resources.Fear_Of_Dog_Park_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Height_Balcony_File.ToString(), Properties.Resources.Fear_Of_Height_Balcony_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Spider_File.ToString(), Properties.Resources.Fear_Of_Spider_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Dark_File.ToString(), Properties.Resources.Fear_Of_Dark_Image);
                            add_scenarios(Properties.Resources.Social_Anxiety_Classroom_File.ToString(), Properties.Resources.Social_Anxiety_Classroom_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Height_Elevator_File.ToString(), Properties.Resources.Fear_Of_Height_Elevator_Image);
                            add_scenarios(Properties.Resources.Fear_Of_Flight_File.ToString(), Properties.Resources.Fear_Of_Flight_Image);
                            add_scenarios(Properties.Resources.Park_Ayda_File.ToString(), Properties.Resources.Park_Ayda_Image);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }
        public void add_scenarios(string file,Image image)
        {
            try
            {
                using (VivosEntities db = new VivosEntities())
                {
                    string asset_imformation1 = null;
                    string[] asset_imformation = null;
                    asset_imformation1 = file;
                    asset_imformation = asset_imformation1.Split(new char[] { '\n' });

                    for (int i = 0; i < asset_imformation.Length - 1; i++)
                    {
                        asset_imformation[i] = asset_imformation[i].Remove(asset_imformation[i].Length - 1);
                    }

                    List<Asset> AssetList = (from x in db.Assets select x).ToList();
                    List<AssetThumbnail> AssetThumbnailList = (from x in db.AssetThumbnails select x).ToList();
                    List<AssetCommand> AssetCommandList = (from x in db.AssetCommands select x).ToList();

                    int command_count = 0;
                    for (int i = 0; i < asset_imformation.Length; i++)
                    {
                        var splitted = asset_imformation[i].Split(new char[] { ',' });
                        if (splitted[0].Contains("Komutlar") == true)
                        {
                            command_count++;
                        }
                    }
                    Asset new_asset = new Asset();
                    AssetThumbnail new_asset_thumbnail = new AssetThumbnail();
                    AssetCommand[] new_asset_command = new AssetCommand[command_count];

                    for (int i = 0; i < new_asset_command.Length; i++)
                    {
                        new_asset_command[i] = new AssetCommand();
                    }

                    Guid new_asset_id = Guid.NewGuid();
                    new_asset.Id = new_asset_id;
                    new_asset.GroupId = new Guid("00000000-0000-0000-0000-000000000000");
                    new_asset.EntryDate = DateTime.Now;
                    int command_add_number = 0;
                    for (int i = 0; i < asset_imformation.Length; i++)
                    {
                        var splitted = asset_imformation[i].Split(new char[] { ',' });

                        if (splitted[0].Contains("Ad") == true)
                        {
                            new_asset.Name = splitted[1];
                            new_asset.EnName = splitted[2];
                            new_asset.ArabicName = splitted[3];
                        }
                        else if (splitted[0].Contains("Açıklama") == true)
                        {
                            new_asset.Description = splitted[1];
                            new_asset.EnDescription = splitted[2];
                            new_asset.ArabicDescription = splitted[3];
                        }
                        else if (splitted[0].Contains("Aktif") == true)
                        {
                            if (splitted[1].Contains("True") == true)
                            {
                                new_asset.Available = true;
                            }
                            else
                            {
                                new_asset.Available = false;
                            }
                        }
                        else if (splitted[0].Contains("Senaryo Dizini") == true)
                        {
                            new_asset.Url = splitted[1];
                        }
                        else if (splitted[0].Contains("Senaryo Exe") == true)
                        {
                            new_asset.Exe = splitted[1];
                        }
                        else if (splitted[0].Contains("Komutlar") == true)
                        {
                            Guid new_asset_command_id = Guid.NewGuid();
                            new_asset_command[command_add_number].Id = new_asset_command_id;
                            new_asset_command[command_add_number].AssetId = new_asset_id;
                            new_asset_command[command_add_number].Description = splitted[1];
                            new_asset_command[command_add_number].EnDescription = splitted[2];
                            new_asset_command[command_add_number].ArabicDescription = splitted[3];
                            new_asset_command[command_add_number].CommandText = splitted[4];
                            try
                            {
                                new_asset_command[command_add_number].Step = Convert.ToByte(splitted[5]);
                            }
                            catch (Exception)
                            {
                                new_asset_command[command_add_number].Step = Convert.ToByte(0);
                            }
                            db.AssetCommands.Add(new_asset_command[command_add_number]);
                            command_add_number++;
                        }
                    }
                    try
                    {
                        Image img = image;
                        Bitmap bmp = new Bitmap(img);
                        ImageConverter converter = new ImageConverter();
                        new_asset_thumbnail.Thumbnail = (byte[])converter.ConvertTo(img, typeof(byte[]));
                        new_asset_thumbnail.AssetId = new_asset_id;
                    }
                    catch (Exception)
                    {
                    }
                    db.Assets.Add(new_asset);
                    db.AssetThumbnails.Add(new_asset_thumbnail);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
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
            btnFrench.Text = resourceManager.GetString("btnFrench", GlobalVariables.uiLanguage);
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
            this.ActiveControl = txtUsername;
        }

        private void btnFrench_Click(object sender, EventArgs e)
        {
            GlobalVariables.uiLanguage = new CultureInfo("fr-FR");
            getres(GlobalVariables.uiLanguage);
        }
    }
}