using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Socket.Client;
using System.Threading;
using System.Net;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace VivoosVR
{
    public partial class New_Session_Controls_Page : MasterForm
    {
        private TimeSpan currentTimeHandler = new TimeSpan();
        private DateTime prevFuncTime = DateTime.Now;
        private Thread cpuThread;
        public bool sud_on_flag = false;
        public bool sud_off_flag = true;
        public bool IsConnectionOpen;
        public double pulse;
        public double gsr;
        public SocketClient new_socket;
        public string data= "Zaman,Pulse,GSR,Marker,Komut,Komut Adımı,SUD";
        public int marker;
        public string komut="Beklemede";
        public int komut_adımı=-1,SUD=0;
        public int flag = 0;

        public New_Session_Controls_Page()
        {     
            InitializeComponent();
            Initialize_SelectedSimulation();
            PlaceSelfOnSecondMonitor();
            fill_datagrid();
            socket();
            taskHandler();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = new DialogResult();
            exit = MessageBox.Show(resourceManager.GetString("msgContinue", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                try
                {
                    if (GlobalVariables.neulogProcess.HasExited == false)
                    {
                        GlobalVariables.neulogProcess.Kill();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (GlobalVariables.sessionProcess.HasExited == false)
                    {
                        GlobalVariables.sessionProcess = applicationControl1.getStartedProcess();
                        var process = Process.GetProcesses().Where(pr => pr.ProcessName.Contains(GlobalVariables.sessionProcess.ProcessName));
                        foreach (var procs in process)
                        {
                            procs.Kill();
                        }
                    }
                }
                catch (Exception)
                {
                }
                Login_Page login_page = new Login_Page();
                this.Hide();
                login_page.Show();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            GlobalVariables.NewSessions_Search_Flag = 0;
            try
            {
                if (GlobalVariables.neulogProcess.HasExited == false)
                {
                    GlobalVariables.neulogProcess.Kill();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (GlobalVariables.sessionProcess.HasExited == false)
                {
                    var process = Process.GetProcesses().Where(pr => pr.ProcessName.Contains(GlobalVariables.sessionProcess.ProcessName));
                    foreach (var procs in process)
                    {
                        procs.Kill();
                    }
                }
            }
            catch (Exception)
            {
            }
            New_Session_Page new_session = new New_Session_Page();
            this.Hide();
            new_session.Show();
        }
        public void fill_datagrid()
        {
            using (VivosEntities db = new VivosEntities())
            {
                commands_datagrid.Rows.Clear();
                commands_datagrid.ColumnCount = 2;
                commands_datagrid.Columns[1].Width = 515;
                commands_datagrid.Columns[0].Visible = false;
                commands_datagrid.Columns[1].Name = resourceManager.GetString("headerStep", GlobalVariables.uiLanguage);

                commands_datagrid.RowTemplate.Height = 30;

                commands_datagrid.BorderStyle = BorderStyle.None;
                commands_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                commands_datagrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                commands_datagrid.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                commands_datagrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                commands_datagrid.BackgroundColor = Color.White;

                commands_datagrid.EnableHeadersVisualStyles = false;
                commands_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                commands_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                commands_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                //add_button();
                for (int i = 0; i < commands_datagrid.ColumnCount; i++)
                {
                    commands_datagrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    commands_datagrid.Columns[i].DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                }
                List<AssetCommand> commandlist = (from x in db.AssetCommands where x.AssetId == GlobalVariables.Asset_Start_ID orderby x.Step select x).ToList();
                for (int i = 0; i < commandlist.Count; i++)
                {
                    i = commands_datagrid.Rows.Add();
                    commands_datagrid.Rows[i].Cells[0].Value = commandlist[i].Id;

                    /*DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(commands_datagrid.Font, FontStyle.Underline);
                    commands_datagrid.Rows[i].DefaultCellStyle = style;*/

                    if (Convert.ToString(GlobalVariables.uiLanguage) == "en-US")
                    {
                        commands_datagrid.Rows[i].Cells[1].Value = commandlist[i].EnDescription;
                   
                    }
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "tr-TR")
                    {
                        commands_datagrid.Rows[i].Cells[1].Value = commandlist[i].Description;
                    }
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "ar-SA")
                    {
                        commands_datagrid.Rows[i].Cells[1].Value = commandlist[i].ArabicDescription;
                    }
                }
            }
        }
        public void add_button()
        {
            DataGridViewButtonColumn btnStart = new DataGridViewButtonColumn();
            commands_datagrid.Columns.Add(btnStart);
            btnStart.HeaderText = resourceManager.GetString("btnStart", GlobalVariables.uiLanguage);
            btnStart.Text = resourceManager.GetString("btnStart", GlobalVariables.uiLanguage);
            btnStart.Name = "startBtn";
            btnStart.UseColumnTextForButtonValue = true;
        }
        public void socket()
        {
            new_socket = SocketClient.Init(VivoosVR.Properties.Settings.Default.soket_ip, VivoosVR.Properties.Settings.Default.soket_port);
            for (int i = 0; i < 10; i++)
            {
                Thread.CurrentThread.Join(1000);
                try
                {
                    new_socket.Connect();
                    new_socket.WaitForConnectionEstablished();
                    IsConnectionOpen = true;
                    break;
                }
                catch (Exception)
                {
                    IsConnectionOpen = false;
                }
            }
        }
        public void taskHandler()
        {
            if (cpuThread == null)
            {
                cpuThread = new Thread(new ThreadStart(this.getPerformanceCounters));
                cpuThread.IsBackground = true;
                cpuThread.Start();
            }
        }
        private void getPerformanceCounters()
        {
            while (true)
            {
                string neulogUrl = string.Format("http://{0}:{1}/NeuLogAPI?GetSensorValue:[GSR],[1],[Pulse],[1]", VivoosVR.Properties.Settings.Default.neulog_ip, VivoosVR.Properties.Settings.Default.neulog_port);
                try
                {
                    HttpWebRequest request = WebRequest.Create(neulogUrl) as HttpWebRequest;
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {

                            currentTimeHandler += DateTime.Now.Subtract(prevFuncTime);
                            prevFuncTime = DateTime.Now;
                            StreamReader sr = new StreamReader(response.GetResponseStream());
                            string rawData = sr.ReadToEnd();
                            if (rawData != "{\"GetSensorValue\":\"False\"}")
                            {
                                var splitted = rawData.Split(new char[] { '[', ',', ']' });
                                string gsrString = splitted[1];
                                string pulseString = splitted[2];
                                gsr = double.Parse(gsrString);
                                pulse = double.Parse(pulseString.Replace(".", ","));
                            }
                            else
                            {
                                gsr = 0;
                                pulse = 0;
                            }
                            data = data + "," + currentTimeHandler + "," + pulse + "," + gsr + "," + marker + "," + komut + "," + komut_adımı + "," + SUD;

                            var cpuPerfCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

                            if (chart1.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate { UpdateChart(); });
                            }
                            Thread.Sleep(VivoosVR.Properties.Settings.Default.sensor_interval);
                        }
                    }
                }
                catch
                {
                    //MessageHelper.Show("Sensör bilgilerinin okunması sırasında hata oluştu.")
                }
            }
        }
        private void UpdateChart()
        {
            if (pulse > chart1.ChartAreas[0].AxisY.Maximum)
            {
                chart1.ChartAreas[0].AxisY.Maximum = pulse + (pulse/2);
            }

            //chart1.Series["Pulse"].Points.AddY(pulse);
            chart1.Series["Pulse"].Points.AddXY(currentTimeHandler.TotalSeconds.ToString("0.00"),pulse);

            if (gsr > chart2.ChartAreas[0].AxisY.Maximum)
            {
                chart2.ChartAreas[0].AxisY.Maximum = gsr + (gsr/2);
            }
            //chart2.Series["GSR"].Points.AddY(gsr);
            chart2.Series["GSR"].Points.AddXY(currentTimeHandler.TotalSeconds.ToString("0.00"), gsr);
            flag++;

            if (flag > 250)
            {
                chart1.Series[0].Points.RemoveAt(0);
                chart2.Series[0].Points.RemoveAt(0);
            }
        }
        
        private void btnSUDkaydet_Click(object sender, EventArgs e)
        {
            SUD = Convert.ToInt32(txtSUD.Text);
            txtSUD.Text = "";
        }

        private void new_session_controls_Load(object sender, EventArgs e)
        {
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            btnSUDSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            lblResetHMD.Text = resourceManager.GetString("lblResetHMD", GlobalVariables.uiLanguage);
            btnSet.Text = resourceManager.GetString("btnSet", GlobalVariables.uiLanguage);
            //this.Text = resourceManager.GetString("formSessionControls", GlobalVariables.uiLanguage);
            this.Text = GlobalVariables.Asset_Start_name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (GlobalVariables.neulogProcess.HasExited == false)
                    {
                        GlobalVariables.neulogProcess.Kill();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (GlobalVariables.sessionProcess.HasExited == false)
                    {
                        var simProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains(GlobalVariables.sessionProcess.ProcessName));
                        foreach (var procs in simProc)
                        {
                            procs.Kill();
                        }
                    }
                }
                catch (Exception)
                {
                }
                using (VivosEntities db = new VivosEntities())
                {
                    List<Session> sessionList = (from x in db.Sessions select x).ToList();
                    Session new_session = new Session();
                    new_session.Id = Guid.NewGuid();
                    new_session.AssetId = GlobalVariables.Asset_Start_ID;
                    new_session.PatientId = GlobalVariables.Session_ID;
                    new_session.Notes = null;
                    new_session.SessionDateTime = DateTime.Now;
                    new_session.SensorData = data;
                    db.Sessions.Add(new_session);
                    db.SaveChanges();
                    DialogResult information = new DialogResult();
                    information = MessageBox.Show(resourceManager.GetString("msgSessionAdded", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                GlobalVariables.NewSessions_Search_Flag = 0;
                Sessions_Page sessions = new Sessions_Page();
                this.Hide();
                sessions.Show();
            }
            catch (Exception)
            {

                
            }
        }
        private void New_SessionControls_Page_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (GlobalVariables.neulogProcess.HasExited == false)
                {
                    GlobalVariables.neulogProcess.Kill();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (GlobalVariables.sessionProcess.HasExited == false)
                {
                    var simProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains(GlobalVariables.sessionProcess.ProcessName));
                    foreach (var procs in simProc)
                    {
                        procs.Kill();
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
                foreach (var procs in vivoosProc)
                {
                    procs.Kill();
                }
            }
            catch (Exception)
            {
            }
        }
        private void commands_datagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    if (GlobalVariables.sessionProcess.HasExited == false)
                    {
                        GlobalVariables.Command_Start_ID = Guid.Parse(commands_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                        using (VivosEntities db = new VivosEntities())
                        {
                            List<AssetCommand> commandList = (from x in db.AssetCommands where x.Id == GlobalVariables.Command_Start_ID select x).ToList();

                            if (Convert.ToString(GlobalVariables.uiLanguage) == "en-US")
                            {
                                komut = commandList[0].EnDescription;
                            }
                            else if (Convert.ToString(GlobalVariables.uiLanguage) == "tr-TR")
                            {
                                komut = commandList[0].Description;
                            }
                            else if (Convert.ToString(GlobalVariables.uiLanguage) == "ar-SA")
                            {
                                komut = commandList[0].ArabicDescription;
                            }
                            komut_adımı = commandList[0].Step;
                            new_socket.Send(commandList[0].CommandText + "\r");
                            new_socket.WaitForSendComplete();
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVariables.sessionProcess.HasExited == false)
                {
                        new_socket.Send("recenter" + "\r");
                        new_socket.WaitForSendComplete();
                }
            }
            catch (Exception)
            {
                
            }
        }
        private void Initialize_SelectedSimulation()
        {
            GlobalVariables.processURL = GlobalVariables.processURL.Remove(GlobalVariables.processURL.Length - 4);
            this.applicationControl1.paraProcess = GlobalVariables.sessionProcess; 
        }
    }
}
