namespace Course.Tools
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.userSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.loginSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.serverSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.SMTPSSLCheckBox = new System.Windows.Forms.CheckBox();
            this.IMAPSSLCheckBox = new System.Windows.Forms.CheckBox();
            this.SMTPPortTextBox = new System.Windows.Forms.TextBox();
            this.SMTPPortLabel = new System.Windows.Forms.Label();
            this.IMAPPortLabel = new System.Windows.Forms.Label();
            this.IMAPPortTextBox = new System.Windows.Forms.TextBox();
            this.SMTPAddressTextBox = new System.Windows.Forms.TextBox();
            this.IMAPAddressTextBox = new System.Windows.Forms.TextBox();
            this.SMTPAddressLabel = new System.Windows.Forms.Label();
            this.IMAPAddressLabel = new System.Windows.Forms.Label();
            this.restoreButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.userSettingsGroupBox.SuspendLayout();
            this.loginSettingsGroupBox.SuspendLayout();
            this.serverSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // userSettingsGroupBox
            // 
            resources.ApplyResources(this.userSettingsGroupBox, "userSettingsGroupBox");
            this.userSettingsGroupBox.Controls.Add(this.emailTextBox);
            this.userSettingsGroupBox.Controls.Add(this.nameTextBox);
            this.userSettingsGroupBox.Controls.Add(this.emailLabel);
            this.userSettingsGroupBox.Controls.Add(this.nameLabel);
            this.userSettingsGroupBox.Name = "userSettingsGroupBox";
            this.userSettingsGroupBox.TabStop = false;
            // 
            // emailTextBox
            // 
            resources.ApplyResources(this.emailTextBox, "emailTextBox");
            this.emailTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "Email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Text = global::Course.Properties.Settings.Default.Email;
            // 
            // nameTextBox
            // 
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Text = global::Course.Properties.Settings.Default.Name;
            // 
            // emailLabel
            // 
            resources.ApplyResources(this.emailLabel, "emailLabel");
            this.emailLabel.Name = "emailLabel";
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Name = "nameLabel";
            // 
            // loginSettingsGroupBox
            // 
            resources.ApplyResources(this.loginSettingsGroupBox, "loginSettingsGroupBox");
            this.loginSettingsGroupBox.Controls.Add(this.passwordTextBox);
            this.loginSettingsGroupBox.Controls.Add(this.usernameTextBox);
            this.loginSettingsGroupBox.Controls.Add(this.passwordLabel);
            this.loginSettingsGroupBox.Controls.Add(this.usernameLabel);
            this.loginSettingsGroupBox.Name = "loginSettingsGroupBox";
            this.loginSettingsGroupBox.TabStop = false;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Text = global::Course.Properties.Settings.Default.Password;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameTextBox
            // 
            resources.ApplyResources(this.usernameTextBox, "usernameTextBox");
            this.usernameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Text = global::Course.Properties.Settings.Default.Username;
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // usernameLabel
            // 
            resources.ApplyResources(this.usernameLabel, "usernameLabel");
            this.usernameLabel.Name = "usernameLabel";
            // 
            // serverSettingsGroupBox
            // 
            resources.ApplyResources(this.serverSettingsGroupBox, "serverSettingsGroupBox");
            this.serverSettingsGroupBox.Controls.Add(this.SMTPSSLCheckBox);
            this.serverSettingsGroupBox.Controls.Add(this.IMAPSSLCheckBox);
            this.serverSettingsGroupBox.Controls.Add(this.SMTPPortTextBox);
            this.serverSettingsGroupBox.Controls.Add(this.SMTPPortLabel);
            this.serverSettingsGroupBox.Controls.Add(this.IMAPPortLabel);
            this.serverSettingsGroupBox.Controls.Add(this.IMAPPortTextBox);
            this.serverSettingsGroupBox.Controls.Add(this.SMTPAddressTextBox);
            this.serverSettingsGroupBox.Controls.Add(this.IMAPAddressTextBox);
            this.serverSettingsGroupBox.Controls.Add(this.SMTPAddressLabel);
            this.serverSettingsGroupBox.Controls.Add(this.IMAPAddressLabel);
            this.serverSettingsGroupBox.Name = "serverSettingsGroupBox";
            this.serverSettingsGroupBox.TabStop = false;
            // 
            // SMTPSSLCheckBox
            // 
            resources.ApplyResources(this.SMTPSSLCheckBox, "SMTPSSLCheckBox");
            this.SMTPSSLCheckBox.Checked = global::Course.Properties.Settings.Default.SMTPSSL;
            this.SMTPSSLCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SMTPSSLCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Course.Properties.Settings.Default, "SMTPSSL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SMTPSSLCheckBox.Name = "SMTPSSLCheckBox";
            this.SMTPSSLCheckBox.UseVisualStyleBackColor = true;
            // 
            // IMAPSSLCheckBox
            // 
            resources.ApplyResources(this.IMAPSSLCheckBox, "IMAPSSLCheckBox");
            this.IMAPSSLCheckBox.Checked = global::Course.Properties.Settings.Default.IMAPSSL;
            this.IMAPSSLCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IMAPSSLCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Course.Properties.Settings.Default, "IMAPSSL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IMAPSSLCheckBox.Name = "IMAPSSLCheckBox";
            this.IMAPSSLCheckBox.UseVisualStyleBackColor = true;
            // 
            // SMTPPortTextBox
            // 
            resources.ApplyResources(this.SMTPPortTextBox, "SMTPPortTextBox");
            this.SMTPPortTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "SMTPPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SMTPPortTextBox.Name = "SMTPPortTextBox";
            this.SMTPPortTextBox.Text = global::Course.Properties.Settings.Default.SMTPPort;
            // 
            // SMTPPortLabel
            // 
            resources.ApplyResources(this.SMTPPortLabel, "SMTPPortLabel");
            this.SMTPPortLabel.Name = "SMTPPortLabel";
            // 
            // IMAPPortLabel
            // 
            resources.ApplyResources(this.IMAPPortLabel, "IMAPPortLabel");
            this.IMAPPortLabel.Name = "IMAPPortLabel";
            // 
            // IMAPPortTextBox
            // 
            resources.ApplyResources(this.IMAPPortTextBox, "IMAPPortTextBox");
            this.IMAPPortTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "IMAPPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IMAPPortTextBox.Name = "IMAPPortTextBox";
            this.IMAPPortTextBox.Text = global::Course.Properties.Settings.Default.IMAPPort;
            // 
            // SMTPAddressTextBox
            // 
            resources.ApplyResources(this.SMTPAddressTextBox, "SMTPAddressTextBox");
            this.SMTPAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "SMTPServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SMTPAddressTextBox.Name = "SMTPAddressTextBox";
            this.SMTPAddressTextBox.Text = global::Course.Properties.Settings.Default.SMTPServer;
            // 
            // IMAPAddressTextBox
            // 
            resources.ApplyResources(this.IMAPAddressTextBox, "IMAPAddressTextBox");
            this.IMAPAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Course.Properties.Settings.Default, "IMAPServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IMAPAddressTextBox.Name = "IMAPAddressTextBox";
            this.IMAPAddressTextBox.Text = global::Course.Properties.Settings.Default.IMAPServer;
            // 
            // SMTPAddressLabel
            // 
            resources.ApplyResources(this.SMTPAddressLabel, "SMTPAddressLabel");
            this.SMTPAddressLabel.Name = "SMTPAddressLabel";
            // 
            // IMAPAddressLabel
            // 
            resources.ApplyResources(this.IMAPAddressLabel, "IMAPAddressLabel");
            this.IMAPAddressLabel.Name = "IMAPAddressLabel";
            // 
            // restoreButton
            // 
            resources.ApplyResources(this.restoreButton, "restoreButton");
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.serverSettingsGroupBox);
            this.Controls.Add(this.loginSettingsGroupBox);
            this.Controls.Add(this.userSettingsGroupBox);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.userSettingsGroupBox.ResumeLayout(false);
            this.userSettingsGroupBox.PerformLayout();
            this.loginSettingsGroupBox.ResumeLayout(false);
            this.loginSettingsGroupBox.PerformLayout();
            this.serverSettingsGroupBox.ResumeLayout(false);
            this.serverSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox userSettingsGroupBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.GroupBox loginSettingsGroupBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.GroupBox serverSettingsGroupBox;
        private System.Windows.Forms.CheckBox SMTPSSLCheckBox;
        private System.Windows.Forms.CheckBox IMAPSSLCheckBox;
        private System.Windows.Forms.TextBox SMTPPortTextBox;
        private System.Windows.Forms.Label SMTPPortLabel;
        private System.Windows.Forms.Label IMAPPortLabel;
        private System.Windows.Forms.TextBox IMAPPortTextBox;
        private System.Windows.Forms.TextBox SMTPAddressTextBox;
        private System.Windows.Forms.TextBox IMAPAddressTextBox;
        private System.Windows.Forms.Label SMTPAddressLabel;
        private System.Windows.Forms.Label IMAPAddressLabel;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Button saveButton;
    }
}