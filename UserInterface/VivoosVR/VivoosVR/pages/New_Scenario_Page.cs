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
    public partial class New_Scenario_Page : MasterForm
    {
        public int CommandCount=0;
        List<TextBox> txtTurkishExplanation = new List<TextBox>();
        List<TextBox> txtCommand = new List<TextBox>();
        List<TextBox> txtStep = new List<TextBox>();
        List<TextBox> txtID = new List<TextBox>();
        List<TextBox> txtEnglishExplanation = new List<TextBox>();
        List<TextBox> txtArabicExplanation = new List<TextBox>();


        public New_Scenario_Page()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            edit_start();
            PlaceSelfOnSecondMonitor();
        }
        public void edit_start()
        {
            if (GlobalVariables.Is_Edit == true)
            {
                using (VivosEntities db = new VivosEntities())
                {
                    List<Asset> AssetList = (from x in db.Assets where x.Id == GlobalVariables.Asset_Start_ID select x).ToList();
                    List<AssetThumbnail> AssetThumbnailList = (from x in db.AssetThumbnails where x.AssetId == GlobalVariables.Asset_Start_ID select x).ToList();

                    txtTurkishName.Text = AssetList[0].Name;
                    txtTurkishDescription.Text= AssetList[0].Description;
                    txtScenarioPath.Text = AssetList[0].Url;
                    txtScenarioExe.Text = AssetList[0].Exe;
                    txtEnglishName.Text = AssetList[0].EnName;
                    txtEnglishDescription.Text = AssetList[0].EnDescription;
                    txtArabicName.Text = AssetList[0].ArabicName;
                    txtArabicDescription.Text = AssetList[0].ArabicDescription;

                    if (AssetList[0].Available==true)
                    {
                        cbAvaliable.Checked = true;
                    }
                    else
                    {
                        cbAvaliable.Checked = false;
                    }

                    try
                    {
                        if (AssetThumbnailList[0].Thumbnail != null)
                        {
                            using (MemoryStream memoryStream = new MemoryStream(AssetThumbnailList[0].Thumbnail))
                            {
                                Bitmap bmp = new Bitmap(memoryStream);
                                pictureBox2.Image = bmp;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                    

                    List<AssetCommand> AssetCommandList = (from x in db.AssetCommands where x.AssetId == GlobalVariables.Asset_Start_ID orderby x.Step select x).ToList();
                    
                    for (int i = 0; i < AssetCommandList.Count; i++)
                    {
                        commandsLayout.RowCount = commandsLayout.RowCount + 1;
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = AssetCommandList[i].Description, Name = "txtTurkishExplanations" + (commandsLayout.RowCount - 1) }, 0, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = AssetCommandList[i].EnDescription, Name = "txtEnglishExplanations" + (commandsLayout.RowCount - 1) }, 1, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = AssetCommandList[i].ArabicDescription, Name = "txtArabicExplanations" + (commandsLayout.RowCount - 1) }, 2, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = AssetCommandList[i].CommandText, Name = "txtCommands" + (commandsLayout.RowCount - 1) }, 3, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Text = Convert.ToByte(AssetCommandList[i].Step).ToString(), Name = "txtSteps" + (commandsLayout.RowCount - 1) }, 4, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Visible = false, Text = AssetCommandList[i].Id.ToString(), Name = "txtID" + (commandsLayout.RowCount - 1) }, 5, commandsLayout.RowCount - 1);
                        txtTurkishExplanation.Add((commandsLayout.Controls.Find(("txtTurkishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtEnglishExplanation.Add((commandsLayout.Controls.Find(("txtEnglishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtArabicExplanation.Add((commandsLayout.Controls.Find(("txtArabicExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtCommand.Add((commandsLayout.Controls.Find(("txtCommands" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtStep.Add((commandsLayout.Controls.Find(("txtSteps" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtID.Add((commandsLayout.Controls.Find(("txtID" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                    }
                }
            }
        }
        private void createTextboxes()
        { 
            commandsLayout.RowCount = commandsLayout.RowCount + 1;
            commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), /*Text = (commandsLayout.RowCount - 1).ToString(),*/ Name = "txtTurkishExplanations" + (commandsLayout.RowCount - 1) }, 0, commandsLayout.RowCount - 1);
            commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20),  Name = "txtEnglishExplanations" + (commandsLayout.RowCount - 1) }, 1, commandsLayout.RowCount - 1);
            commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20),  Name = "txtArabicExplanations" + (commandsLayout.RowCount - 1) }, 2, commandsLayout.RowCount - 1);
            commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Name = "txtCommands" + (commandsLayout.RowCount - 1) }, 3, commandsLayout.RowCount - 1);
            commandsLayout.Controls.Add(new TextBox() { Name = "txtSteps" + (commandsLayout.RowCount - 1) }, 4, commandsLayout.RowCount - 1);
            commandsLayout.Controls.Add(new TextBox() { Visible = false, Text = "00000000-0000-0000-0000-000000000000", Name = "txtID" + (commandsLayout.RowCount - 1) }, 5, commandsLayout.RowCount - 1);

            txtTurkishExplanation.Add((commandsLayout.Controls.Find(("txtTurkishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
            txtEnglishExplanation.Add((commandsLayout.Controls.Find(("txtEnglishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
            txtArabicExplanation.Add((commandsLayout.Controls.Find(("txtArabicExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
            txtCommand.Add((commandsLayout.Controls.Find(("txtCommands" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
            txtStep.Add((commandsLayout.Controls.Find(("txtSteps" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
            txtID.Add((commandsLayout.Controls.Find(("txtID" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);

        }
        private void new_scenario_Load(object sender, EventArgs e)
        {
            lblAvailable.Text = resourceManager.GetString("lblAvailable", GlobalVariables.uiLanguage);
            btnImport.Text = resourceManager.GetString("btnImport", GlobalVariables.uiLanguage);
            lblTurkishName.Text = resourceManager.GetString("lblTurkishName", GlobalVariables.uiLanguage);
            lblEnglishName.Text = resourceManager.GetString("lblEnglishName", GlobalVariables.uiLanguage);
            lblArabicName.Text = resourceManager.GetString("lblArabicName", GlobalVariables.uiLanguage);
            lblTurkishDescription.Text = resourceManager.GetString("lblTurkishDescription", GlobalVariables.uiLanguage);
            lblEnglishDescription.Text = resourceManager.GetString("lblEnglishDescription", GlobalVariables.uiLanguage);
            lblArabicDescription.Text = resourceManager.GetString("lblArabicDescription", GlobalVariables.uiLanguage);
            lblThumbnail.Text = resourceManager.GetString("lblThumbnail", GlobalVariables.uiLanguage);
            lblScenarioExe.Text = resourceManager.GetString("lblScenarioExe", GlobalVariables.uiLanguage);
            lblScenarioPath.Text = resourceManager.GetString("lblScenarioPath", GlobalVariables.uiLanguage);
            lblCommands.Text = resourceManager.GetString("lblCommands", GlobalVariables.uiLanguage);
            btnSelectFile.Text = resourceManager.GetString("btnSelectFile", GlobalVariables.uiLanguage);
            btnBack.Text = resourceManager.GetString("btnBack", GlobalVariables.uiLanguage);
            btnSave.Text = resourceManager.GetString("btnSave", GlobalVariables.uiLanguage);
            btnExit.Text = resourceManager.GetString("btnExit", GlobalVariables.uiLanguage);
            this.Text = resourceManager.GetString("btnSettings", GlobalVariables.uiLanguage);
            btnDeleteCommands.Text = resourceManager.GetString("btnDeleteCommands", GlobalVariables.uiLanguage);

            commandsLayout.ColumnCount = 6;
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            commandsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            commandsLayout.Controls.Add(new Label() { Text = resourceManager.GetString("lblTurkishDescription", GlobalVariables.uiLanguage) }, 0, 0);
            commandsLayout.Controls.Add(new Label() { Text = resourceManager.GetString("lblEnglishDescription", GlobalVariables.uiLanguage) }, 1, 0);
            commandsLayout.Controls.Add(new Label() { Text = resourceManager.GetString("lblArabicDescription", GlobalVariables.uiLanguage) }, 2, 0);
            commandsLayout.Controls.Add(new Label() { Text = resourceManager.GetString("lblCommands", GlobalVariables.uiLanguage) }, 3, 0);
            commandsLayout.Controls.Add(new Label() { Text = resourceManager.GetString("lblStep", GlobalVariables.uiLanguage) }, 4, 0);
            commandsLayout.Controls.Add(new Label() { Visible=false, Text = "ID" }, 5, 0);

            if (GlobalVariables.Is_Edit==false)
            {
                txtScenarioPath.Text = VivoosVR.Properties.Settings.Default.senaryo_dizin;
                commandsLayout.RowCount = commandsLayout.RowCount + 1;
                commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20),  Name = "txtTurkishExplanations" + (commandsLayout.RowCount - 1) }, 0, commandsLayout.RowCount - 1);
                commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20),  Name = "txtEnglishExplanations" + (commandsLayout.RowCount - 1) }, 1, commandsLayout.RowCount - 1);
                commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Name = "txtArabicExplanations" + (commandsLayout.RowCount - 1) }, 2, commandsLayout.RowCount - 1);
                commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Name = "txtCommands" + (commandsLayout.RowCount - 1) }, 3, commandsLayout.RowCount - 1);
                commandsLayout.Controls.Add(new TextBox() {  Name = "txtSteps" + (commandsLayout.RowCount - 1) }, 4, commandsLayout.RowCount - 1);
                commandsLayout.Controls.Add(new TextBox() { Visible=false, Text = "00000000-0000-0000-0000-000000000000", Name = "txtID" + (commandsLayout.RowCount - 1) }, 5, commandsLayout.RowCount - 1);

                txtTurkishExplanation.Add((commandsLayout.Controls.Find(("txtTurkishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                txtEnglishExplanation.Add((commandsLayout.Controls.Find(("txtEnglishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                txtArabicExplanation.Add((commandsLayout.Controls.Find(("txtArabicExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                txtCommand.Add((commandsLayout.Controls.Find(("txtCommands" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                txtStep.Add((commandsLayout.Controls.Find(("txtSteps" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                txtID.Add((commandsLayout.Controls.Find(("txtID" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);

            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            createTextboxes();
        }

        private void btnDosyaSeç_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.png;*.jpeg";
            dosya.ShowDialog();
            string DosyaYolu = dosya.FileName;
            pictureBox2.ImageLocation = DosyaYolu;
            
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
            GlobalVariables.NewSessions_Search_Flag = 0;
            Admin_Page admin_page = new Admin_Page();
            this.Hide();
            admin_page.Show();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            /*if (GlobalVariables.Is_Edit==false)
            {
                txtScenarioExe.Text = txtTurkishName.Text + ".exe";
                txtScenarioPath.Text = VivoosVR.Properties.Settings.Default.senaryo_dizin + txtTurkishName.Text + Convert.ToString(@"\") + txtScenarioExe.Text ;
            }  */
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Is_Edit == false)
            { 
                using (VivosEntities db = new VivosEntities())
                {
                    List<Asset> AssetList = (from x in db.Assets select x).ToList();
                    List<AssetThumbnail> AssetThumbnailList = (from x in db.AssetThumbnails select x).ToList();
                    List<AssetCommand> AssetCommandList = (from x in db.AssetCommands select x).ToList();

                    Asset new_asset = new Asset();
                    AssetThumbnail new_asset_thumbnail = new AssetThumbnail();
                    AssetCommand[] new_asset_command = new AssetCommand[commandsLayout.RowCount-1];

                    for (int i = 0; i < new_asset_command.Length; i++)
                    {
                        new_asset_command[i] = new AssetCommand();
                    }

                    Guid new_asset_id= Guid.NewGuid();

                    new_asset.Id = new_asset_id;
                    new_asset.GroupId = new Guid("00000000-0000-0000-0000-000000000000");
                    new_asset.Name = txtTurkishName.Text;
                    new_asset.EnName = txtEnglishName.Text;
                    new_asset.ArabicName = txtArabicName.Text;
                    new_asset.Description = txtTurkishDescription.Text;
                    new_asset.EnDescription = txtEnglishDescription.Text;
                    new_asset.ArabicDescription = txtArabicDescription.Text;
                    if (cbAvaliable.Checked==true)
                    {
                        new_asset.Available = true;
                    }
                    else
                    {
                        new_asset.Available = false;   
                    }
                          

                    new_asset.Url = txtScenarioPath.Text;
                    new_asset.Exe = txtScenarioExe.Text;
                    new_asset.EntryDate = DateTime.Now;

                    try
                    {
                        if (pictureBox2.Image!=null)
                        {
                            Image img = pictureBox2.Image;
                            Bitmap bmp = new Bitmap(img);
                            ImageConverter converter = new ImageConverter();
                            new_asset_thumbnail.Thumbnail = (byte[])converter.ConvertTo(img, typeof(byte[]));
                            new_asset_thumbnail.AssetId = new_asset_id;
                        }
                        else
                        {
                            new_asset_thumbnail.Thumbnail = null;
                            new_asset_thumbnail.AssetId = new_asset_id;
                        }
                        
                        
                    }
                    catch (Exception)
                    {

                        
                    }
                    for (int i = 0; i < commandsLayout.RowCount - 2; i++)
                    {
                        Guid new_asset_command_id = Guid.NewGuid();
                        new_asset_command[i].Id = new_asset_command_id;
                        new_asset_command[i].AssetId = new_asset_id;
                        new_asset_command[i].CommandText = txtCommand[i].Text;
                        new_asset_command[i].Description = txtTurkishExplanation[i].Text;
                        new_asset_command[i].EnDescription = txtEnglishExplanation[i].Text;
                        new_asset_command[i].ArabicDescription = txtArabicExplanation[i].Text;
                        try
                        {
                            new_asset_command[i].Step = Convert.ToByte(txtStep[i].Text);
                        }
                        catch (Exception)
                        {

                            new_asset_command[i].Step = Convert.ToByte(0);
                        }
                            
                        
                       
                        
                        db.AssetCommands.Add(new_asset_command[i]);

                    }

                    db.Assets.Add(new_asset);
                    db.AssetThumbnails.Add(new_asset_thumbnail);
                    db.SaveChanges();
                    DialogResult information = new DialogResult();
                    information = MessageBox.Show(resourceManager.GetString("msgScenarioAdded", GlobalVariables.uiLanguage));
                    if (information == DialogResult.OK)
                    {
                        Admin_Page admin_page = new Admin_Page();
                        this.Hide();
                        admin_page.Show();
                    }


                }
                
            }
            else
            {
                
                using (VivosEntities db = new VivosEntities())
                {
                    List<Asset> AssetList = (from x in db.Assets select x).ToList();
                    List<AssetThumbnail> AssetThumbnailList = (from x in db.AssetThumbnails select x).ToList();
                    List<AssetCommand> AssetCommandList = (from x in db.AssetCommands select x).ToList();

                    AssetCommand[] new_asset_command = new AssetCommand[commandsLayout.RowCount - 1];

                    for (int i = 0; i < new_asset_command.Length; i++)
                    {
                        new_asset_command[i] = new AssetCommand();
                    }

                    Asset edit_asset= (from x in db.Assets where x.Id == GlobalVariables.Asset_Start_ID select x).SingleOrDefault();
                    if(edit_asset!=null)
                    { 
                        edit_asset.Name = txtTurkishName.Text;
                        edit_asset.EnName = txtEnglishName.Text;
                        edit_asset.ArabicName = txtArabicName.Text;
                        edit_asset.Description = txtTurkishDescription.Text;
                        edit_asset.EnDescription = txtEnglishDescription.Text;
                        edit_asset.ArabicDescription = txtArabicDescription.Text;
                        edit_asset.Url = txtScenarioPath.Text;
                        edit_asset.Exe = txtScenarioExe.Text;
                        edit_asset.ModifyDate = DateTime.Now;
                        if (cbAvaliable.Checked==true)
                        {
                            edit_asset.Available = true;
                        }
                        else
                        {
                            edit_asset.Available = false;
                        }
                    }
                    AssetThumbnail edit_asset_thumbnail = (from x in db.AssetThumbnails where x.AssetId == GlobalVariables.Asset_Start_ID select x).SingleOrDefault();
                    if (edit_asset_thumbnail != null)
                    {
                        if (pictureBox2.Image!=null)
                        {
                            Image img = pictureBox2.Image;
                            Bitmap bmp = new Bitmap(img);
                            ImageConverter converter = new ImageConverter();
                            try
                            {
                                edit_asset_thumbnail.Thumbnail = (byte[])converter.ConvertTo(img, typeof(byte[]));
                            }
                            catch
                            {

                            }
                        }
                         
                    }

                    for (int i = 0; i < commandsLayout.RowCount-2; i++)
                    {
                        int flag = 0;
                        for (int j = 0; j < AssetCommandList.Count; j++)
                        {
                            if (AssetCommandList[j].Id== Guid.Parse(txtID[i].Text))
                            {
                                AssetCommandList[j].CommandText = txtCommand[i].Text;
                                AssetCommandList[j].Description = txtTurkishExplanation[i].Text;
                                AssetCommandList[j].EnDescription = txtEnglishExplanation[i].Text;
                                AssetCommandList[j].ArabicDescription = txtArabicExplanation[i].Text;
                                AssetCommandList[j].Step = Convert.ToByte(txtStep[i].Text);
                                flag = 1;
                                break;
                            }
                        }
                        if (flag==0)
                        {
                            Guid new_asset_command_id = Guid.NewGuid();
                            new_asset_command[i].Id = new_asset_command_id;
                            new_asset_command[i].AssetId = GlobalVariables.Asset_Start_ID;
                            new_asset_command[i].CommandText = txtCommand[i].Text;
                            new_asset_command[i].Description = txtTurkishExplanation[i].Text;
                            new_asset_command[i].EnDescription = txtEnglishExplanation[i].Text;
                            new_asset_command[i].ArabicDescription = txtArabicExplanation[i].Text;
                            new_asset_command[i].Step = Convert.ToByte(txtStep[i].Text);
                            db.AssetCommands.Add(new_asset_command[i]);
                        }
                    }
                    db.SaveChanges();
                    DialogResult information = new DialogResult();
                    information = MessageBox.Show(resourceManager.GetString("msgScenarioEdited", GlobalVariables.uiLanguage));
                    if (information == DialogResult.OK)
                    {
                        Admin_Page admin_page = new Admin_Page();
                        this.Hide();
                        admin_page.Show();
                    }
                }

            }
        }

        private void new_scenario_FormClosing(object sender, FormClosingEventArgs e)
        {
            var vivoosProc = Process.GetProcesses().Where(pr => pr.ProcessName.Contains("VivoosVR"));
            foreach (var procs in vivoosProc)
            {
                procs.Kill();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                commandsLayout.Controls.Clear();
                commandsLayout.RowCount = commandsLayout.RowCount - 1;
                txtTurkishExplanation.Clear();
                txtEnglishExplanation.Clear();
                txtArabicExplanation.Clear();
                txtCommand.Clear();
                txtStep.Clear();
                txtID.Clear();
                string[] asset_imformation = null;
                OpenFileDialog file1 = new OpenFileDialog();
                file1.InitialDirectory = ".\\desktop";
                file1.Filter = "Text|*.txt";
                if (file1.ShowDialog() == DialogResult.OK)
                {
                    asset_imformation = System.IO.File.ReadAllLines(file1.FileName);
                }
                for (int i = 0; i < asset_imformation.Length; i++)
                {
                    var splitted = asset_imformation[i].Split(new char[] { ',' });
                    if (splitted[0].Contains(resourceManager.GetString("lblName", GlobalVariables.uiLanguage)) == true)
                    {
                        txtTurkishName.Text = splitted[1];
                        txtEnglishName.Text = splitted[2];
                        txtArabicName.Text = splitted[3];
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("lblDescription", GlobalVariables.uiLanguage)) == true)
                    {
                        txtTurkishDescription.Text = splitted[1];
                        txtEnglishDescription.Text = splitted[2];
                        txtArabicDescription.Text = splitted[3];
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("lblScenarioPath", GlobalVariables.uiLanguage)) == true)
                    {
                        txtScenarioPath.Text = splitted[1];
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("lblScenarioExe", GlobalVariables.uiLanguage)) == true)
                    {
                        txtScenarioExe.Text = splitted[1];
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("lblAvailable", GlobalVariables.uiLanguage)) == true)
                    {
                        if (splitted[1].Contains("True") == true)
                        {
                            cbAvaliable.Checked = true;
                        }
                        else
                        {
                            cbAvaliable.Checked = false;
                        }
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("lblCommands", GlobalVariables.uiLanguage)) == true)
                    {
                        commandsLayout.RowCount = commandsLayout.RowCount + 1;
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = splitted[1], Name = "txtTurkishExplanations" + (commandsLayout.RowCount - 1) }, 0, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = splitted[2], Name = "txtEnglishExplanations" + (commandsLayout.RowCount - 1) }, 1, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = splitted[3], Name = "txtArabicExplanations" + (commandsLayout.RowCount - 1) }, 2, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Size = new Size(242, 20), Text = splitted[4], Name = "txtCommands" + (commandsLayout.RowCount - 1) }, 3, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Text = Convert.ToByte(splitted[5]).ToString(), Name = "txtSteps" + (commandsLayout.RowCount - 1) }, 4, commandsLayout.RowCount - 1);
                        commandsLayout.Controls.Add(new TextBox() { Visible = false, Text = "00000000-0000-0000-0000-000000000000", Name = "txtID" + (commandsLayout.RowCount - 1) }, 5, commandsLayout.RowCount - 1);
                        txtTurkishExplanation.Add((commandsLayout.Controls.Find(("txtTurkishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtEnglishExplanation.Add((commandsLayout.Controls.Find(("txtEnglishExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtArabicExplanation.Add((commandsLayout.Controls.Find(("txtArabicExplanations" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtCommand.Add((commandsLayout.Controls.Find(("txtCommands" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtStep.Add((commandsLayout.Controls.Find(("txtSteps" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                        txtID.Add((commandsLayout.Controls.Find(("txtID" + (commandsLayout.RowCount - 1)), true)[0]) as TextBox);
                    }
                    else if (splitted[0].Contains(resourceManager.GetString("headerThumbnail", GlobalVariables.uiLanguage)) == true)
                    {
                        /* using (MemoryStream memoryStream = new MemoryStream(Convert.ToByte(splitted[1])))
                         {
                             Bitmap bmp = new Bitmap(memoryStream);
                             pictureBox2.Image = bmp;
                         }*/
                    }
                }
            }
            catch (Exception)
            {

               
            }
            
        }

        private void btnDeleteCommands_Click(object sender, EventArgs e)
        {
            Command_Delete_Page command_delete_page = new Command_Delete_Page();
            this.Hide();
            command_delete_page.Show();  
        }
    }
}
