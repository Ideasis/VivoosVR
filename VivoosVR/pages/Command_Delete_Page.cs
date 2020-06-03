using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VivoosVR
{
    public partial class Command_Delete_Page : MasterForm
    {
        public Command_Delete_Page()
        {
            InitializeComponent();
            PlaceSelfOnSecondMonitor();
            fill_datagrid();
        }

        private void Command_Delete_Page_Load(object sender, EventArgs e)
        {
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("btnDeleteCommands", GlobalVariables.uiLanguage);
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

                add_button();
                for (int i = 0; i < commands_datagrid.ColumnCount-1; i++)
                {
                    commands_datagrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    commands_datagrid.Columns[i].DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                }
                List<AssetCommand> commandlist = (from x in db.AssetCommands where x.AssetId == GlobalVariables.Asset_Start_ID orderby x.Step select x).ToList();
                for (int i = 0; i < commandlist.Count; i++)
                {
                    i = commands_datagrid.Rows.Add();
                    commands_datagrid.Rows[i].Cells[0].Value = commandlist[i].Id;

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
                    else if (Convert.ToString(GlobalVariables.uiLanguage) == "fr-FR")
                    {
                        commands_datagrid.Rows[i].Cells[1].Value = commandlist[i].FrDescription;
                    }
                }
            }
        }
        public void add_button()
        {
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            commands_datagrid.Columns.Add(btnDelete);
            btnDelete.HeaderText = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Text = resourceManager.GetString("headerDelete", GlobalVariables.uiLanguage);
            btnDelete.Name = "deleteBtn";
            btnDelete.UseColumnTextForButtonValue = true;
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
            New_Scenario_Page new_scenario_page = new New_Scenario_Page();
            this.Hide();
            new_scenario_page.Show();
        }

        private void Command_Delete_Page_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void commands_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                /*try
                {*/
                    DialogResult delete = new DialogResult();
                    delete = MessageBox.Show(resourceManager.GetString("msgIscommandDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgWarning", GlobalVariables.uiLanguage), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (delete == DialogResult.Yes)
                    {
                        GlobalVariables.Command_ID = Guid.Parse(commands_datagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                        using (VivosEntities db = new VivosEntities())
                        {
                            AssetCommand command = db.AssetCommands.First(x => x.Id == GlobalVariables.Command_ID);
                            db.AssetCommands.Remove(command);
                            db.SaveChanges();
                            //string key = null;
                            fill_datagrid();
                            DialogResult information = new DialogResult();
                            information = MessageBox.Show(resourceManager.GetString("msgCommandDeleted", GlobalVariables.uiLanguage), resourceManager.GetString("msgInformation", GlobalVariables.uiLanguage), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                //}
                /*catch (Exception)
                {


                }*/

            }
        }
    }
}
