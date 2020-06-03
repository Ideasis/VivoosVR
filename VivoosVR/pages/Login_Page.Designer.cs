namespace VivoosVR
{
    partial class Login_Page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Page));
            this.tlpInput = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tlpCancelLogin = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.pbVivoosVR = new System.Windows.Forms.PictureBox();
            this.tlpLanguages = new System.Windows.Forms.TableLayoutPanel();
            this.btnTurkish = new System.Windows.Forms.Button();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.btnArabic = new System.Windows.Forms.Button();
            this.btnFrench = new System.Windows.Forms.Button();
            this.tlpPassword = new System.Windows.Forms.TableLayoutPanel();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tlpInput.SuspendLayout();
            this.tlpCancelLogin.SuspendLayout();
            this.tlp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVivoosVR)).BeginInit();
            this.tlpLanguages.SuspendLayout();
            this.tlpPassword.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpInput
            // 
            this.tlpInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpInput.BackColor = System.Drawing.Color.Transparent;
            this.tlpInput.ColumnCount = 2;
            this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.78947F));
            this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.21053F));
            this.tlpInput.Controls.Add(this.lblUsername, 0, 0);
            this.tlpInput.Controls.Add(this.lblPassword, 0, 1);
            this.tlpInput.Controls.Add(this.txtUsername, 1, 0);
            this.tlpInput.Controls.Add(this.txtPassword, 1, 1);
            this.tlpInput.Location = new System.Drawing.Point(26, 30);
            this.tlpInput.Margin = new System.Windows.Forms.Padding(2);
            this.tlpInput.Name = "tlpInput";
            this.tlpInput.RowCount = 2;
            this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.0566F));
            this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.9434F));
            this.tlpInput.Size = new System.Drawing.Size(407, 51);
            this.tlpInput.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblUsername.Location = new System.Drawing.Point(30, 4);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(97, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Kullanıcı Adı ";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPassword.Location = new System.Drawing.Point(87, 30);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(40, 16);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Şifre";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUsername.Location = new System.Drawing.Point(131, 2);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(274, 20);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPassword.Location = new System.Drawing.Point(131, 27);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(274, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // tlpCancelLogin
            // 
            this.tlpCancelLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpCancelLogin.ColumnCount = 3;
            this.tlpCancelLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.89954F));
            this.tlpCancelLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.94064F));
            this.tlpCancelLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.93151F));
            this.tlpCancelLogin.Controls.Add(this.btnCancel, 1, 0);
            this.tlpCancelLogin.Controls.Add(this.btnLogin, 2, 0);
            this.tlpCancelLogin.Location = new System.Drawing.Point(15, 8);
            this.tlpCancelLogin.Margin = new System.Windows.Forms.Padding(2);
            this.tlpCancelLogin.Name = "tlpCancelLogin";
            this.tlpCancelLogin.RowCount = 1;
            this.tlpCancelLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCancelLogin.Size = new System.Drawing.Size(438, 38);
            this.tlpCancelLogin.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(168, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 26);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(167)))), ((int)(((byte)(38)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.Location = new System.Drawing.Point(286, 2);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(103, 26);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Giriş";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tlp
            // 
            this.tlp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlp.ColumnCount = 3;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.pbVivoosVR, 1, 0);
            this.tlp.Location = new System.Drawing.Point(514, 244);
            this.tlp.Margin = new System.Windows.Forms.Padding(2);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Size = new System.Drawing.Size(438, 57);
            this.tlp.TabIndex = 4;
            // 
            // pbVivoosVR
            // 
            this.pbVivoosVR.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbVivoosVR.Image = global::VivoosVR.Properties.Resources.logo2;
            this.pbVivoosVR.Location = new System.Drawing.Point(116, 2);
            this.pbVivoosVR.Margin = new System.Windows.Forms.Padding(2);
            this.pbVivoosVR.Name = "pbVivoosVR";
            this.pbVivoosVR.Size = new System.Drawing.Size(182, 53);
            this.pbVivoosVR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbVivoosVR.TabIndex = 0;
            this.pbVivoosVR.TabStop = false;
            // 
            // tlpLanguages
            // 
            this.tlpLanguages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpLanguages.ColumnCount = 6;
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.45622F));
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.54378F));
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tlpLanguages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpLanguages.Controls.Add(this.btnTurkish, 1, 0);
            this.tlpLanguages.Controls.Add(this.btnEnglish, 2, 0);
            this.tlpLanguages.Controls.Add(this.btnArabic, 3, 0);
            this.tlpLanguages.Controls.Add(this.btnFrench, 4, 0);
            this.tlpLanguages.Location = new System.Drawing.Point(465, 562);
            this.tlpLanguages.Name = "tlpLanguages";
            this.tlpLanguages.RowCount = 1;
            this.tlpLanguages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLanguages.Size = new System.Drawing.Size(436, 66);
            this.tlpLanguages.TabIndex = 5;
            // 
            // btnTurkish
            // 
            this.btnTurkish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTurkish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTurkish.FlatAppearance.BorderSize = 0;
            this.btnTurkish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTurkish.Image = ((System.Drawing.Image)(resources.GetObject("btnTurkish.Image")));
            this.btnTurkish.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTurkish.Location = new System.Drawing.Point(120, 3);
            this.btnTurkish.Name = "btnTurkish";
            this.btnTurkish.Size = new System.Drawing.Size(67, 60);
            this.btnTurkish.TabIndex = 1;
            this.btnTurkish.Text = "Türkçe";
            this.btnTurkish.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTurkish.UseVisualStyleBackColor = true;
            this.btnTurkish.Click += new System.EventHandler(this.btnTurkish_Click);
            // 
            // btnEnglish
            // 
            this.btnEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnglish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnglish.FlatAppearance.BorderSize = 0;
            this.btnEnglish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnglish.Image = global::VivoosVR.Properties.Resources.E_52px;
            this.btnEnglish.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnglish.Location = new System.Drawing.Point(193, 3);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(65, 60);
            this.btnEnglish.TabIndex = 0;
            this.btnEnglish.Text = "İngilizce";
            this.btnEnglish.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnglish.UseVisualStyleBackColor = true;
            this.btnEnglish.Click += new System.EventHandler(this.btnEnglish_Click);
            // 
            // btnArabic
            // 
            this.btnArabic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArabic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnArabic.FlatAppearance.BorderSize = 0;
            this.btnArabic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArabic.Image = global::VivoosVR.Properties.Resources.A_52px;
            this.btnArabic.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnArabic.Location = new System.Drawing.Point(264, 3);
            this.btnArabic.Name = "btnArabic";
            this.btnArabic.Size = new System.Drawing.Size(68, 60);
            this.btnArabic.TabIndex = 2;
            this.btnArabic.Text = "Arapça";
            this.btnArabic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnArabic.UseVisualStyleBackColor = true;
            this.btnArabic.Click += new System.EventHandler(this.btnArabic_Click);
            // 
            // btnFrench
            // 
            this.btnFrench.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFrench.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFrench.FlatAppearance.BorderSize = 0;
            this.btnFrench.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrench.Image = global::VivoosVR.Properties.Resources.fr;
            this.btnFrench.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFrench.Location = new System.Drawing.Point(338, 3);
            this.btnFrench.Name = "btnFrench";
            this.btnFrench.Size = new System.Drawing.Size(67, 60);
            this.btnFrench.TabIndex = 3;
            this.btnFrench.Text = "Fransızca";
            this.btnFrench.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFrench.UseVisualStyleBackColor = true;
            this.btnFrench.Click += new System.EventHandler(this.btnFrench_Click);
            // 
            // tlpPassword
            // 
            this.tlpPassword.ColumnCount = 2;
            this.tlpPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.69725F));
            this.tlpPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.30275F));
            this.tlpPassword.Controls.Add(this.btnChangePassword, 0, 0);
            this.tlpPassword.Location = new System.Drawing.Point(17, 58);
            this.tlpPassword.Name = "tlpPassword";
            this.tlpPassword.RowCount = 1;
            this.tlpPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPassword.Size = new System.Drawing.Size(436, 39);
            this.tlpPassword.TabIndex = 6;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(179)))), ((int)(((byte)(66)))));
            this.btnChangePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChangePassword.Location = new System.Drawing.Point(178, 3);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(197, 27);
            this.btnChangePassword.TabIndex = 2;
            this.btnChangePassword.Text = "Şifre Değiştir";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.tlpInput);
            this.panel1.Location = new System.Drawing.Point(439, 321);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 100);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.panel2.Controls.Add(this.tlpCancelLogin);
            this.panel2.Controls.Add(this.tlpPassword);
            this.panel2.Location = new System.Drawing.Point(439, 418);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 100);
            this.panel2.TabIndex = 0;
            // 
            // Login_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1426, 839);
            this.Controls.Add(this.tlpLanguages);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login_Page";
            this.Text = "Giriş";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Page_Load);
            this.tlpInput.ResumeLayout(false);
            this.tlpInput.PerformLayout();
            this.tlpCancelLogin.ResumeLayout(false);
            this.tlp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVivoosVR)).EndInit();
            this.tlpLanguages.ResumeLayout(false);
            this.tlpPassword.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpInput;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TableLayoutPanel tlpCancelLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.PictureBox pbVivoosVR;
        private System.Windows.Forms.TableLayoutPanel tlpLanguages;
        private System.Windows.Forms.Button btnEnglish;
        private System.Windows.Forms.Button btnTurkish;
        private System.Windows.Forms.Button btnArabic;
        private System.Windows.Forms.TableLayoutPanel tlpPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnFrench;
    }
}

