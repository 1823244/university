namespace Course.Tools
{
    partial class NewMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMail));
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.attachButton = new System.Windows.Forms.Button();
            this.filesTextBox = new System.Windows.Forms.TextBox();
            this.attachLabel = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toTextBox
            // 
            resources.ApplyResources(this.toTextBox, "toTextBox");
            this.toTextBox.Name = "toTextBox";
            // 
            // toLabel
            // 
            resources.ApplyResources(this.toLabel, "toLabel");
            this.toLabel.Name = "toLabel";
            // 
            // subjectTextBox
            // 
            resources.ApplyResources(this.subjectTextBox, "subjectTextBox");
            this.subjectTextBox.Name = "subjectTextBox";
            // 
            // subjectLabel
            // 
            resources.ApplyResources(this.subjectLabel, "subjectLabel");
            this.subjectLabel.Name = "subjectLabel";
            // 
            // messageTextBox
            // 
            resources.ApplyResources(this.messageTextBox, "messageTextBox");
            this.messageTextBox.Name = "messageTextBox";
            // 
            // attachButton
            // 
            resources.ApplyResources(this.attachButton, "attachButton");
            this.attachButton.Name = "attachButton";
            this.attachButton.UseVisualStyleBackColor = true;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // filesTextBox
            // 
            resources.ApplyResources(this.filesTextBox, "filesTextBox");
            this.filesTextBox.Name = "filesTextBox";
            this.filesTextBox.TabStop = false;
            // 
            // attachLabel
            // 
            resources.ApplyResources(this.attachLabel, "attachLabel");
            this.attachLabel.Name = "attachLabel";
            // 
            // sendButton
            // 
            resources.ApplyResources(this.sendButton, "sendButton");
            this.sendButton.Name = "sendButton";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // NewMail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.filesTextBox);
            this.Controls.Add(this.attachLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.toLabel);
            this.Name = "NewMail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewMail_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.TextBox filesTextBox;
        private System.Windows.Forms.Label attachLabel;
        private System.Windows.Forms.Button sendButton;
    }
}