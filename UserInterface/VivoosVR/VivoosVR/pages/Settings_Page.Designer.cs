namespace VivoosVR
{
    partial class Settings_Page
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSensorInterval = new System.Windows.Forms.TextBox();
            this.txtSensorVisible = new System.Windows.Forms.TextBox();
            this.txtNeulogPort = new System.Windows.Forms.TextBox();
            this.txtNeulogIP = new System.Windows.Forms.TextBox();
            this.txtNeulogPath = new System.Windows.Forms.TextBox();
            this.txtScenarioPath = new System.Windows.Forms.TextBox();
            this.txtSocketIP = new System.Windows.Forms.TextBox();
            this.lblNeulogPath = new System.Windows.Forms.Label();
            this.lblScenarioPath = new System.Windows.Forms.Label();
            this.lblSocketIP = new System.Windows.Forms.Label();
            this.lblSocketPort = new System.Windows.Forms.Label();
            this.lblNeulogIP = new System.Windows.Forms.Label();
            this.lblNeulogPort = new System.Windows.Forms.Label();
            this.lblSensorVisible = new System.Windows.Forms.Label();
            this.lblSensorInterval = new System.Windows.Forms.Label();
            this.txtSocketPort = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.92593F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.07407F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 922F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1436, 42);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::VivoosVR.Properties.Resources.logo2;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 37);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.07895F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.92105F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel4.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnBack, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(516, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(917, 36);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.Location = new System.Drawing.Point(762, 2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 32);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Çıkış";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.Location = new System.Drawing.Point(604, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(153, 30);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Geri";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 793);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1436, 46);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1246, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.07042F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.92958F));
            this.tableLayoutPanel3.Controls.Add(this.txtSensorInterval, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtSensorVisible, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.txtNeulogPort, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtNeulogIP, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtNeulogPath, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtScenarioPath, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtSocketIP, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNeulogPath, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblScenarioPath, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblSocketIP, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblSocketPort, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblNeulogIP, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.lblNeulogPort, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblSensorVisible, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.lblSensorInterval, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtSocketPort, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(489, 181);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 8;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.45055F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.54945F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(318, 372);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // txtSensorInterval
            // 
            this.txtSensorInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSensorInterval.Location = new System.Drawing.Point(146, 341);
            this.txtSensorInterval.Name = "txtSensorInterval";
            this.txtSensorInterval.Size = new System.Drawing.Size(169, 20);
            this.txtSensorInterval.TabIndex = 15;
            // 
            // txtSensorVisible
            // 
            this.txtSensorVisible.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSensorVisible.Location = new System.Drawing.Point(146, 297);
            this.txtSensorVisible.Name = "txtSensorVisible";
            this.txtSensorVisible.Size = new System.Drawing.Size(169, 20);
            this.txtSensorVisible.TabIndex = 14;
            // 
            // txtNeulogPort
            // 
            this.txtNeulogPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNeulogPort.Location = new System.Drawing.Point(146, 251);
            this.txtNeulogPort.Name = "txtNeulogPort";
            this.txtNeulogPort.Size = new System.Drawing.Size(169, 20);
            this.txtNeulogPort.TabIndex = 13;
            // 
            // txtNeulogIP
            // 
            this.txtNeulogIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNeulogIP.Location = new System.Drawing.Point(146, 204);
            this.txtNeulogIP.Name = "txtNeulogIP";
            this.txtNeulogIP.Size = new System.Drawing.Size(169, 20);
            this.txtNeulogIP.TabIndex = 12;
            // 
            // txtNeulogPath
            // 
            this.txtNeulogPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNeulogPath.Location = new System.Drawing.Point(146, 152);
            this.txtNeulogPath.Name = "txtNeulogPath";
            this.txtNeulogPath.Size = new System.Drawing.Size(169, 20);
            this.txtNeulogPath.TabIndex = 11;
            // 
            // txtScenarioPath
            // 
            this.txtScenarioPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtScenarioPath.Location = new System.Drawing.Point(146, 103);
            this.txtScenarioPath.Name = "txtScenarioPath";
            this.txtScenarioPath.Size = new System.Drawing.Size(169, 20);
            this.txtScenarioPath.TabIndex = 10;
            // 
            // txtSocketIP
            // 
            this.txtSocketIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSocketIP.Location = new System.Drawing.Point(146, 12);
            this.txtSocketIP.Name = "txtSocketIP";
            this.txtSocketIP.Size = new System.Drawing.Size(169, 20);
            this.txtSocketIP.TabIndex = 9;
            // 
            // lblNeulogPath
            // 
            this.lblNeulogPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNeulogPath.AutoSize = true;
            this.lblNeulogPath.Location = new System.Drawing.Point(3, 149);
            this.lblNeulogPath.Name = "lblNeulogPath";
            this.lblNeulogPath.Size = new System.Drawing.Size(125, 26);
            this.lblNeulogPath.TabIndex = 6;
            this.lblNeulogPath.Text = "Neulog\'un Bilgisayardaki Yeri";
            // 
            // lblScenarioPath
            // 
            this.lblScenarioPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblScenarioPath.AutoSize = true;
            this.lblScenarioPath.Location = new System.Drawing.Point(3, 106);
            this.lblScenarioPath.Name = "lblScenarioPath";
            this.lblScenarioPath.Size = new System.Drawing.Size(74, 13);
            this.lblScenarioPath.TabIndex = 4;
            this.lblScenarioPath.Text = "Senaryo Dizini";
            // 
            // lblSocketIP
            // 
            this.lblSocketIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSocketIP.AutoSize = true;
            this.lblSocketIP.Location = new System.Drawing.Point(3, 16);
            this.lblSocketIP.Name = "lblSocketIP";
            this.lblSocketIP.Size = new System.Drawing.Size(48, 13);
            this.lblSocketIP.TabIndex = 0;
            this.lblSocketIP.Text = "Soket IP";
            // 
            // lblSocketPort
            // 
            this.lblSocketPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSocketPort.AutoSize = true;
            this.lblSocketPort.Location = new System.Drawing.Point(3, 61);
            this.lblSocketPort.Name = "lblSocketPort";
            this.lblSocketPort.Size = new System.Drawing.Size(57, 13);
            this.lblSocketPort.TabIndex = 1;
            this.lblSocketPort.Text = "Soket Port";
            // 
            // lblNeulogIP
            // 
            this.lblNeulogIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNeulogIP.AutoSize = true;
            this.lblNeulogIP.Location = new System.Drawing.Point(3, 207);
            this.lblNeulogIP.Name = "lblNeulogIP";
            this.lblNeulogIP.Size = new System.Drawing.Size(54, 13);
            this.lblNeulogIP.TabIndex = 5;
            this.lblNeulogIP.Text = "Neulog IP";
            // 
            // lblNeulogPort
            // 
            this.lblNeulogPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNeulogPort.AutoSize = true;
            this.lblNeulogPort.Location = new System.Drawing.Point(3, 254);
            this.lblNeulogPort.Name = "lblNeulogPort";
            this.lblNeulogPort.Size = new System.Drawing.Size(63, 13);
            this.lblNeulogPort.TabIndex = 3;
            this.lblNeulogPort.Text = "Neulog Port";
            // 
            // lblSensorVisible
            // 
            this.lblSensorVisible.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSensorVisible.AutoSize = true;
            this.lblSensorVisible.Location = new System.Drawing.Point(3, 294);
            this.lblSensorVisible.Name = "lblSensorVisible";
            this.lblSensorVisible.Size = new System.Drawing.Size(134, 26);
            this.lblSensorVisible.TabIndex = 2;
            this.lblSensorVisible.Text = "Sensör Bİlgilerinin Görünür Olacağı Zaman Aralığı";
            // 
            // lblSensorInterval
            // 
            this.lblSensorInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSensorInterval.AutoSize = true;
            this.lblSensorInterval.Location = new System.Drawing.Point(3, 338);
            this.lblSensorInterval.Name = "lblSensorInterval";
            this.lblSensorInterval.Size = new System.Drawing.Size(120, 26);
            this.lblSensorInterval.TabIndex = 7;
            this.lblSensorInterval.Text = "Sensörlerden Ne Sıklıta Veri Okunacağı (ms)";
            // 
            // txtSocketPort
            // 
            this.txtSocketPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSocketPort.Location = new System.Drawing.Point(146, 58);
            this.txtSocketPort.Name = "txtSocketPort";
            this.txtSocketPort.Size = new System.Drawing.Size(169, 20);
            this.txtSocketPort.TabIndex = 8;
            // 
            // Settings_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1436, 839);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Settings_Page";
            this.Text = "Ayarlar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Page_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblNeulogPath;
        private System.Windows.Forms.Label lblScenarioPath;
        private System.Windows.Forms.Label lblSocketIP;
        private System.Windows.Forms.Label lblSocketPort;
        private System.Windows.Forms.Label lblNeulogIP;
        private System.Windows.Forms.Label lblNeulogPort;
        private System.Windows.Forms.Label lblSensorVisible;
        private System.Windows.Forms.Label lblSensorInterval;
        private System.Windows.Forms.TextBox txtSensorInterval;
        private System.Windows.Forms.TextBox txtSensorVisible;
        private System.Windows.Forms.TextBox txtNeulogPort;
        private System.Windows.Forms.TextBox txtNeulogIP;
        private System.Windows.Forms.TextBox txtNeulogPath;
        private System.Windows.Forms.TextBox txtScenarioPath;
        private System.Windows.Forms.TextBox txtSocketIP;
        private System.Windows.Forms.TextBox txtSocketPort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBack;
    }
}