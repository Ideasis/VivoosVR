using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Diagnostics;
using frm = System.Windows.Forms;
namespace VivoosVR
{
    public partial class New_Session_Page : MasterForm
    {
        public New_Session_Page()
        {   
            string key = null;
            InitializeComponent();
            fill_datagrid(key);
            PlaceSelfOnSecondMonitor();
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
        private void btnBack_Click(object sender, EventArgs e)
        {
            GlobalVariables.Sessions_Search_Flag = 0;
            Sessions_Page sessions = new Sessions_Page();
            this.Hide();
            sessions.Show();
        }
        public void fill_datagrid(string key)
        {
            using (VivosEntities db = new VivosEntities())
            {
                newsession_datagrid.Rows.Clear();
                newsession_datagrid.ColumnCount = 4;
                newsession_datagrid.Columns[0].Width = 300;
                newsession_datagrid.Columns[1].Visible = false;
                newsession_datagrid.Columns[2].Name = resourceManager.GetString("headerScenario", GlobalVariables.uiLanguage);
                newsession_datagrid.Columns[2].Width = 150;
                newsession_datagrid.Columns[3].Name = resourceManager.GetString("headerExplanation", GlobalVariables.uiLanguage);
                newsession_datagrid.RowTemplate.Height = 100;

                newsession_datagrid.BorderStyle = BorderStyle.None;
                newsession_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                newsession_datagrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                newsession_datagrid.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                newsession_datagrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                newsession_datagrid.BackgroundColor = Color.White;

                newsession_datagrid.EnableHeadersVisualStyles = false;
                newsession_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                newsession_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                newsession_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                add_button();
                newsession_datagrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for (int i = 0; i < newsession_datagrid.ColumnCount - 1; i++)
                {
                    newsession_datagrid.Columns[i].DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                }
                List<Asset> assetlist = (from x in db.Assets where x.Available==true orderby x.Name select x).ToList();
                List<Asset> assetlist2 = (from x in db.Assets where x.Name.StartsWith(key) & x.Available==true orderby x.Name select x).ToList();
                List<AssetThumbnail> assetThumbnail = (from x in db.AssetThumbnails select x).ToList();
                List<Asset> keyList = null;
                if (GlobalVariables.NewSessions_Search_Flag == 0)
                    keyList = assetlist;
                else
                    keyList = assetlist2;
                for (int i = 0; i < keyList.Count; i++)
                {
                    i = newsession_datagrid.Rows.Add();
                    newsession_datagrid.Rows[i].Cells[1].Value = keyList[i].Id;
                    for (int j = 0; j < assetThumbnail.Count; j++)
                    {
                        if (assetThumbnail[j].AssetId == keyList[i].Id)
                        {
                            if (assetThumbnail[j].Thumbnail!=null)
                            {
                                using (MemoryStream memoryStream = new MemoryStream(assetThumbnail[j].Thumbnail))
                                {
                                    Bitmap bmp = new Bitmap(memoryStream);
                                    newsession_datagrid.Rows[i].Cells[0].Value = bmp;
                                }
                            }
                            
                        }
                    }
                    if (Convert.ToString( GlobalVariables.uiLanguage)=="en-US")
                    {
                        newsession_datagrid.Rows[i].Cells[2].Value = keyList[i].EnName;
                        newsession_datagrid.Rows[i].Cells[3].Value = keyList[i].EnDescription;
                    }
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "tr-TR")
                    {
                        newsession_datagrid.Rows[i].Cells[2].Value = keyList[i].Name;
                        newsession_datagrid.Rows[i].Cells[3].Value = keyList[i].Description;
                    }
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "ar-SA")
                    {
                        newsession_datagrid.Rows[i].Cells[2].Value = keyList[i].ArabicName;
                        newsession_datagrid.Rows[i].Cells[3].Value = keyList[i].ArabicDescription;
                    }
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "fr-FR")
                    {
                        newsession_datagrid.Rows[i].Cells[2].Value = keyList[i].FrName;
                        newsession_datagrid.Rows[i].Cells[3].Value = keyList[i].FrDescription;
                    }
                }
            }
        }
        public void add_button()
        {
            DataGridViewButtonColumn btnStart = new DataGridViewButtonColumn();
            newsession_datagrid.Columns.Add(btnStart);
            btnStart.HeaderText = resourceManager.GetString("btnStart", GlobalVariables.uiLanguage);
            btnStart.Text = resourceManager.GetString("btnStart", GlobalVariables.uiLanguage);
            btnStart.Name = "startBtn";
            btnStart.UseColumnTextForButtonValue = true;
           
            
            
            /*btnStart.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 167, 38);
            btnStart.FlatStyle = FlatStyle.Flat;*/

        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            GlobalVariables.NewSessions_Search_Flag = 1;
            fill_datagrid(txtSearch.Text.ToString());
        }

        private void new_session_Load(object sender, EventArgs e)
        {
            lblSearch.Text = resourceManager.GetString("lblSearch", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formNewSession", GlobalVariables.uiLanguage);
        }

        private void new_session_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void newsession_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    GlobalVariables.Asset_Start_ID = Guid.Parse(newsession_datagrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                    using (VivosEntities db = new VivosEntities())
                    {
                        List<Asset> assets = (from x in db.Assets select x).ToList();
                        for (int i = 0; i < assets.Count; i++)
                        {
                            if (GlobalVariables.Asset_Start_ID == assets[i].Id)
                            {
                                try
                                {

                                    GlobalVariables.neulogProcess = System.Diagnostics.Process.Start(VivoosVR.Properties.Settings.Default.neulog_dizin);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Neulog" + " " + VivoosVR.Properties.Settings.Default.neulog_dizin + " " + resourceManager.GetString("msgCannotFound", GlobalVariables.uiLanguage));
                                }
                                try
                                {
                                    if (Convert.ToString(GlobalVariables.uiLanguage) == "en-US")
                                    {
                                        GlobalVariables.Asset_Start_name = assets[i].EnName;
                                    }
                                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "tr-TR")
                                    {
                                        GlobalVariables.Asset_Start_name = assets[i].Name;
                                    }
                                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "ar-SA")
                                    {
                                        GlobalVariables.Asset_Start_name = assets[i].ArabicName;
                                    }
                                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "fr-FR")
                                    {
                                        GlobalVariables.Asset_Start_name = assets[i].FrName;
                                    }
                                    GlobalVariables.sessionProcess = System.Diagnostics.Process.Start(@assets[i].Url);
                                    GlobalVariables.processURL = @assets[i].Url;
                                    New_Session_Controls_Page new_session_controls = new New_Session_Controls_Page();
                                    new_session_controls.Show();
                                    this.Hide();
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(resourceManager.GetString("headerScenario", GlobalVariables.uiLanguage) + assets[i].Url + " " + resourceManager.GetString("msgCannotFound", GlobalVariables.uiLanguage));
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    
                }
               
            }
        }
    }
}
