using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Diagnostics;

namespace VivoosVR
{
    public partial class Sessions_Page : MasterForm
    {
        string file;
        public Sessions_Page()
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
            GlobalVariables.Is_Session = false;
            GlobalVariables.Patients_Search_Flag = 0;
            Patients_Page patients = new Patients_Page();
            this.Hide();
            patients.Show();
        }
        public void fill_datagrid(string key)
        {
            using (VivosEntities db = new VivosEntities())
            {
                sessions_datagrid.Rows.Clear();
                sessions_datagrid.ColumnCount = 3;
                sessions_datagrid.Columns[0].Visible = false;
                sessions_datagrid.Columns[1].Name = resourceManager.GetString("headerDateTime", GlobalVariables.uiLanguage);
                sessions_datagrid.Columns[2].Name = resourceManager.GetString("headerScenario", GlobalVariables.uiLanguage);
                sessions_datagrid.RowTemplate.Height = 30;

                sessions_datagrid.BorderStyle = BorderStyle.None;
                sessions_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                sessions_datagrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                sessions_datagrid.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                sessions_datagrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                sessions_datagrid.BackgroundColor = Color.White;

                sessions_datagrid.EnableHeadersVisualStyles = false;
                sessions_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                sessions_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                sessions_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                add_button();
                for (int i = 0; i < sessions_datagrid.ColumnCount - 2; i++)
                {
                    sessions_datagrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    sessions_datagrid.Columns[i].DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                }
                List<Session> sessionlist1 = (from x in db.Sessions join s in db.Assets on x.AssetId equals s.Id where x.PatientId == GlobalVariables.Session_ID orderby s.Name select x).ToList();
                List<Asset> assetlist = (from x in db.Assets select x).ToList();
                List<Session> sessionlist2 = (from x in db.Sessions join s in db.Assets on x.AssetId equals s.Id where s.Name.StartsWith(key) && x.PatientId == GlobalVariables.Session_ID orderby s.Name select x).ToList();
                List<Session> keyList = null;
                if (GlobalVariables.Sessions_Search_Flag == 0)
                    keyList = sessionlist1;
                else
                    keyList = sessionlist2;
                for (int i = 0; i < keyList.Count; i++)
                {
                    i = sessions_datagrid.Rows.Add();
                    sessions_datagrid.Rows[i].Cells[0].Value = keyList[i].Id;
                    if (keyList[i].SessionDateTime != null)
                    {
                        sessions_datagrid.Rows[i].Cells[1].Value = Convert.ToDateTime(keyList[i].SessionDateTime).ToString("dd/MM/yyyy HH:mm ", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                        sessions_datagrid.Rows[i].Cells[1].Value = null;
                    for (int j = 0; j < assetlist.Count; j++)
                    {
                        if (keyList[i].AssetId == assetlist[j].Id)
                        {
                            if (Convert.ToString(GlobalVariables.uiLanguage) == "en-US")
                            {
                                sessions_datagrid.Rows[i].Cells[2].Value = assetlist[j].EnName;
                            }
                            else if (Convert.ToString(GlobalVariables.uiLanguage) == "tr-TR")
                            {
                                sessions_datagrid.Rows[i].Cells[2].Value = assetlist[j].Name;
                            }
                            else if (Convert.ToString(GlobalVariables.uiLanguage) == "ar-SA")
                            {
                                sessions_datagrid.Rows[i].Cells[2].Value = assetlist[j].ArabicName;
                            }
                            break;
                        }
                    }
                }
            }
        }
        public void add_button()
        {
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            sessions_datagrid.Columns.Add(btnDelete);
            btnDelete.HeaderText = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Text = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Name = "deleteBtn";
            btnDelete.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnData = new DataGridViewButtonColumn();
            sessions_datagrid.Columns.Add(btnData);
            btnData.HeaderText = resourceManager.GetString("headerDataDownload", GlobalVariables.uiLanguage);
            btnData.Text = resourceManager.GetString("headerDataDownload", GlobalVariables.uiLanguage);
            btnData.Name = "dataBtn";
            btnData.UseColumnTextForButtonValue = true;
        }
        public void createDoc()
        {
            SaveFileDialog file1 = new SaveFileDialog();
            file1.InitialDirectory = ".\\desktop";
            file1.Filter = "Excel Dosyası|*.xls";
            file1.FileName = GlobalVariables.Session_ID_name + "_" + GlobalVariables.Session_Data_name + "_" + GlobalVariables.Session_Data_date;
            if (file1.ShowDialog() == DialogResult.OK)
            {
                file = file1.FileName.ToString();
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("First Sheet");
                using (VivosEntities db = new VivosEntities())
                {
                    int k = 0;
                    List<Session> sessionlist = (from x in db.Sessions where x.Id == GlobalVariables.Session_Data_ID select x).ToList();
                    if (sessionlist == null)
                        MessageBox.Show(resourceManager.GetString("msgData", GlobalVariables.uiLanguage));
                    else
                    {
                        string data = sessionlist[0].SensorData.ToString();
                        data = data.Replace("\r\n", ",");
                        string[] data1 = (data.Split(','));
                        worksheet.Cells.ColumnWidth[0, 0] = 4000;
                        worksheet.Cells.ColumnWidth[0, 1] = 4000;
                        worksheet.Cells.ColumnWidth[0, 2] = 4000;
                        worksheet.Cells[0, 0] = new Cell(GlobalVariables.Session_ID_name);
                        worksheet.Cells[0, 1] = new Cell(GlobalVariables.Session_Data_name);
                        worksheet.Cells[0, 2] = new Cell(GlobalVariables.Session_Data_date);
                        int i = 2;
                        for (int a = 0; a < data1.Length / 7; a++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                worksheet.Cells[i, j] = new Cell(data1[k].ToString());
                                k++;
                            }
                            i++;
                        }
                        workbook.Worksheets.Add(worksheet);
                        workbook.Save(file);
                        Workbook book = Workbook.Load(file);
                        Worksheet sheet = book.Worksheets[0];
                        MessageBox.Show(resourceManager.GetString("msgDataDownloaded", GlobalVariables.uiLanguage));
                    }
                }
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.Sessions_Search_Flag = 1;
            fill_datagrid(txtSearch.Text.ToString());
        }
        private void sessions_Load(object sender, EventArgs e)
        {
            lblSearch.Text = resourceManager.GetString("lblSearch", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnNewSession.Text = resourceManager.GetString("btnNewSession", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("formSessions", GlobalVariables.uiLanguage);
        }

        private void sessions_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void btnNewSession_Click(object sender, EventArgs e)
        {
            GlobalVariables.NewSessions_Search_Flag = 0;
            New_Session_Page new_session = new New_Session_Page();
            this.Hide();
            new_session.Show();
        }

        private void sessions_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                try
                {
                    DialogResult delete = new DialogResult();
                    delete = MessageBox.Show(resourceManager.GetString("msgIsSessionDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (delete == DialogResult.Yes)
                    {
                        GlobalVariables.Session_Data_ID = Guid.Parse(sessions_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                        using (VivosEntities db = new VivosEntities())
                        {
                            List<Session> sessionlist = (from x in db.Sessions where x.Id == GlobalVariables.Session_Data_ID select x).ToList();
                            Session session = db.Sessions.First(x => x.Id == GlobalVariables.Session_Data_ID);
                            db.Sessions.Remove(session);
                            db.SaveChanges();
                            GlobalVariables.Sessions_Search_Flag = 0;
                            string key = null;
                            fill_datagrid(key);
                            DialogResult information = new DialogResult();
                            information = MessageBox.Show(resourceManager.GetString("msgSessionDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            else if (e.ColumnIndex == 4)
            {
                try
                {
                    GlobalVariables.Session_Data_ID = Guid.Parse(sessions_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    using (VivosEntities db = new VivosEntities())
                    {
                        List<Session> sessionlist = (from x in db.Sessions where x.Id == GlobalVariables.Session_Data_ID select x).ToList();
                        List<Asset> assetlist = (from x in db.Assets select x).ToList();
                        for (int i = 0; i < assetlist.Count; i++)
                        {
                            if (assetlist[i].Id == sessionlist[0].AssetId)
                            {
                                GlobalVariables.Session_Data_name = assetlist[0].Name;
                                break;
                            }
                        }
                        if (sessionlist[0].SessionDateTime != null)
                        {
                            GlobalVariables.Session_Data_date = Convert.ToDateTime(sessionlist[0].SessionDateTime).ToString("dd.MM.yyyy HH.mm ", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                            GlobalVariables.Session_Data_date = null;
                    }
                    createDoc();
                }
                catch (Exception)
                {

                    
                }
               
            }
        }
    }
}
