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
    public partial class Settings_Page : MasterForm
    {
        public Settings_Page()
        {
            InitializeComponent();
            Load_Settings();
            PlaceSelfOnSecondMonitor();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            VivoosVR.Properties.Settings.Default.soket_ip = txtSocketIP.Text;
            VivoosVR.Properties.Settings.Default.soket_port = Convert.ToInt32(txtSocketPort.Text);
            VivoosVR.Properties.Settings.Default.senaryo_dizin = txtScenarioPath.Text;
            VivoosVR.Properties.Settings.Default.neulog_dizin = txtNeulogPath.Text;
            VivoosVR.Properties.Settings.Default.neulog_ip = txtNeulogIP.Text;
            VivoosVR.Properties.Settings.Default.neulog_port = Convert.ToInt32(txtNeulogPort.Text);
            VivoosVR.Properties.Settings.Default.sensor_visible = txtSensorVisible.Text;
            VivoosVR.Properties.Settings.Default.sensor_interval = Convert.ToInt32(txtSensorInterval.Text);
            DialogResult information = new DialogResult();
            information = MessageBox.Show(resourceManager.GetString("msgSettingsSaved", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Load_Settings()
        {
            txtSocketIP.Text = VivoosVR.Properties.Settings.Default.soket_ip;
            txtSocketPort.Text = VivoosVR.Properties.Settings.Default.soket_port.ToString();
            txtScenarioPath.Text = VivoosVR.Properties.Settings.Default.senaryo_dizin;
            txtNeulogPath.Text = VivoosVR.Properties.Settings.Default.neulog_dizin;
            txtNeulogIP.Text = VivoosVR.Properties.Settings.Default.neulog_ip;
            txtNeulogPort.Text = VivoosVR.Properties.Settings.Default.neulog_port.ToString();
            txtSensorVisible.Text = VivoosVR.Properties.Settings.Default.sensor_visible;
            txtSensorInterval.Text = VivoosVR.Properties.Settings.Default.sensor_interval.ToString();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            GlobalVariables.Sessions_Search_Flag = 0;
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
        private void settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }
        private void Settings_Page_Load(object sender, EventArgs e)
        {
            lblSocketIP.Text = resourceManager.GetString("lblSocketIP", GlobalVariables.uiLanguage);
            lblSocketPort.Text = resourceManager.GetString("lblSocketPort", GlobalVariables.uiLanguage);
            lblScenarioPath.Text = resourceManager.GetString("lblScenarioPath", GlobalVariables.uiLanguage);
            lblNeulogPath.Text = resourceManager.GetString("lblNeulogPath", GlobalVariables.uiLanguage);
            lblNeulogIP.Text = resourceManager.GetString("lblNeulogIP", GlobalVariables.uiLanguage);
            lblNeulogPort.Text = resourceManager.GetString("lblNeulogPort", GlobalVariables.uiLanguage);
            lblSensorVisible.Text = resourceManager.GetString("lblSensorVisible", GlobalVariables.uiLanguage);
            lblSensorInterval.Text = resourceManager.GetString("lblSensorInterval", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("btnSettings", GlobalVariables.uiLanguage);
        }
    }
}
