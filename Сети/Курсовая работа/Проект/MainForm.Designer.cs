namespace Course
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.maillistDataGridView = new System.Windows.Forms.DataGridView();
            this.saveToButton = new System.Windows.Forms.Button();
            this.attachmentsTextBox = new System.Windows.Forms.TextBox();
            this.mailRichTextBox = new System.Windows.Forms.RichTextBox();
            this.topToolStrip = new System.Windows.Forms.ToolStrip();
            this.getMailToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newMailToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mailDeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mailSaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.foldersTreeView = new System.Windows.Forms.TreeView();
            this.bottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.totalToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalNumberToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.separatorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.unseenToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.unseenNumberToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maillistDataGridView)).BeginInit();
            this.topToolStrip.SuspendLayout();
            this.bottomStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            resources.ApplyResources(this.mainSplitContainer, "mainSplitContainer");
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            resources.ApplyResources(this.mainSplitContainer.Panel1, "mainSplitContainer.Panel1");
            this.mainSplitContainer.Panel1.Controls.Add(this.maillistDataGridView);
            // 
            // mainSplitContainer.Panel2
            // 
            resources.ApplyResources(this.mainSplitContainer.Panel2, "mainSplitContainer.Panel2");
            this.mainSplitContainer.Panel2.Controls.Add(this.saveToButton);
            this.mainSplitContainer.Panel2.Controls.Add(this.attachmentsTextBox);
            this.mainSplitContainer.Panel2.Controls.Add(this.mailRichTextBox);
            // 
            // maillistDataGridView
            // 
            resources.ApplyResources(this.maillistDataGridView, "maillistDataGridView");
            this.maillistDataGridView.AllowUserToAddRows = false;
            this.maillistDataGridView.AllowUserToOrderColumns = true;
            this.maillistDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.maillistDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.maillistDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maillistDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.Subject,
            this.Sender});
            this.maillistDataGridView.Name = "maillistDataGridView";
            this.maillistDataGridView.ReadOnly = true;
            this.maillistDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.maillistDataGridView.SelectionChanged += new System.EventHandler(this.maillistDataGridView_SelectionChanged);
            // 
            // saveToButton
            // 
            resources.ApplyResources(this.saveToButton, "saveToButton");
            this.saveToButton.Name = "saveToButton";
            this.saveToButton.UseVisualStyleBackColor = true;
            this.saveToButton.Click += new System.EventHandler(this.saveToButton_Click);
            // 
            // attachmentsTextBox
            // 
            resources.ApplyResources(this.attachmentsTextBox, "attachmentsTextBox");
            this.attachmentsTextBox.Name = "attachmentsTextBox";
            this.attachmentsTextBox.ReadOnly = true;
            // 
            // mailRichTextBox
            // 
            resources.ApplyResources(this.mailRichTextBox, "mailRichTextBox");
            this.mailRichTextBox.Name = "mailRichTextBox";
            // 
            // topToolStrip
            // 
            resources.ApplyResources(this.topToolStrip, "topToolStrip");
            this.topToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getMailToolStripButton,
            this.toolStripSeparator1,
            this.newMailToolStripButton,
            this.toolStripSeparator2,
            this.mailDeleteToolStripButton,
            this.toolStripSeparator3,
            this.mailSaveToolStripButton,
            this.toolStripSeparator4,
            this.settingsToolStripButton});
            this.topToolStrip.Name = "topToolStrip";
            // 
            // getMailToolStripButton
            // 
            resources.ApplyResources(this.getMailToolStripButton, "getMailToolStripButton");
            this.getMailToolStripButton.Image = global::Course.Properties.Resources.icon__refresh;
            this.getMailToolStripButton.Name = "getMailToolStripButton";
            this.getMailToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.getMailToolStripButton.Click += new System.EventHandler(this.getMailToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // newMailToolStripButton
            // 
            resources.ApplyResources(this.newMailToolStripButton, "newMailToolStripButton");
            this.newMailToolStripButton.Image = global::Course.Properties.Resources.icon__mail_new;
            this.newMailToolStripButton.Name = "newMailToolStripButton";
            this.newMailToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.newMailToolStripButton.Click += new System.EventHandler(this.newMailToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // mailDeleteToolStripButton
            // 
            resources.ApplyResources(this.mailDeleteToolStripButton, "mailDeleteToolStripButton");
            this.mailDeleteToolStripButton.Image = global::Course.Properties.Resources.icon__mail_delete;
            this.mailDeleteToolStripButton.Name = "mailDeleteToolStripButton";
            this.mailDeleteToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.mailDeleteToolStripButton.Click += new System.EventHandler(this.mailDeleteToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // mailSaveToolStripButton
            // 
            resources.ApplyResources(this.mailSaveToolStripButton, "mailSaveToolStripButton");
            this.mailSaveToolStripButton.Image = global::Course.Properties.Resources.icon__mail_save;
            this.mailSaveToolStripButton.Name = "mailSaveToolStripButton";
            this.mailSaveToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.mailSaveToolStripButton.Click += new System.EventHandler(this.mailSaveToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // settingsToolStripButton
            // 
            resources.ApplyResources(this.settingsToolStripButton, "settingsToolStripButton");
            this.settingsToolStripButton.Image = global::Course.Properties.Resources.icon__settings;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // foldersTreeView
            // 
            resources.ApplyResources(this.foldersTreeView, "foldersTreeView");
            this.foldersTreeView.Name = "foldersTreeView";
            this.foldersTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.foldersTreeView_AfterSelect);
            // 
            // bottomStatusStrip
            // 
            resources.ApplyResources(this.bottomStatusStrip, "bottomStatusStrip");
            this.bottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalToolStripStatusLabel,
            this.totalNumberToolStripStatusLabel,
            this.separatorToolStripStatusLabel,
            this.unseenToolStripStatusLabel,
            this.unseenNumberToolStripStatusLabel});
            this.bottomStatusStrip.Name = "bottomStatusStrip";
            // 
            // totalToolStripStatusLabel
            // 
            resources.ApplyResources(this.totalToolStripStatusLabel, "totalToolStripStatusLabel");
            this.totalToolStripStatusLabel.Name = "totalToolStripStatusLabel";
            // 
            // totalNumberToolStripStatusLabel
            // 
            resources.ApplyResources(this.totalNumberToolStripStatusLabel, "totalNumberToolStripStatusLabel");
            this.totalNumberToolStripStatusLabel.Name = "totalNumberToolStripStatusLabel";
            // 
            // separatorToolStripStatusLabel
            // 
            resources.ApplyResources(this.separatorToolStripStatusLabel, "separatorToolStripStatusLabel");
            this.separatorToolStripStatusLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.separatorToolStripStatusLabel.Name = "separatorToolStripStatusLabel";
            // 
            // unseenToolStripStatusLabel
            // 
            resources.ApplyResources(this.unseenToolStripStatusLabel, "unseenToolStripStatusLabel");
            this.unseenToolStripStatusLabel.Name = "unseenToolStripStatusLabel";
            // 
            // unseenNumberToolStripStatusLabel
            // 
            resources.ApplyResources(this.unseenNumberToolStripStatusLabel, "unseenNumberToolStripStatusLabel");
            this.unseenNumberToolStripStatusLabel.Name = "unseenNumberToolStripStatusLabel";
            // 
            // UID
            // 
            resources.ApplyResources(this.UID, "UID");
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            // 
            // Subject
            // 
            resources.ApplyResources(this.Subject, "Subject");
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            // 
            // Sender
            // 
            resources.ApplyResources(this.Sender, "Sender");
            this.Sender.Name = "Sender";
            this.Sender.ReadOnly = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.bottomStatusStrip);
            this.Controls.Add(this.foldersTreeView);
            this.Controls.Add(this.topToolStrip);
            this.Name = "MainForm";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maillistDataGridView)).EndInit();
            this.topToolStrip.ResumeLayout(false);
            this.topToolStrip.PerformLayout();
            this.bottomStatusStrip.ResumeLayout(false);
            this.bottomStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip topToolStrip;
        private System.Windows.Forms.TreeView foldersTreeView;
        private System.Windows.Forms.StatusStrip bottomStatusStrip;
        private System.Windows.Forms.DataGridView maillistDataGridView;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.RichTextBox mailRichTextBox;
        private System.Windows.Forms.ToolStripButton getMailToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton newMailToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mailDeleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton mailSaveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.ToolStripStatusLabel totalToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel totalNumberToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel separatorToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel unseenToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel unseenNumberToolStripStatusLabel;
        private System.Windows.Forms.Button saveToButton;
        private System.Windows.Forms.TextBox attachmentsTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sender;
    }
}

