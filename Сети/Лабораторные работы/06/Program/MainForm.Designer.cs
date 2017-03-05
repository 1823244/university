namespace Lab6
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.domain = new System.Windows.Forms.TextBox();
            this.findIP = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.findDomain = new System.Windows.Forms.Button();
            this.ip = new System.Windows.Forms.TextBox();
            this.aboutDHCP = new System.Windows.Forms.Button();
            this.resetIP = new System.Windows.Forms.Button();
            this.resultInfo = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.findIP);
            this.groupBox1.Controls.Add(this.domain);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP по домену";
            // 
            // domain
            // 
            this.domain.Location = new System.Drawing.Point(7, 20);
            this.domain.Name = "domain";
            this.domain.Size = new System.Drawing.Size(140, 20);
            this.domain.TabIndex = 0;
            // 
            // findIP
            // 
            this.findIP.Location = new System.Drawing.Point(7, 47);
            this.findIP.Name = "findIP";
            this.findIP.Size = new System.Drawing.Size(75, 23);
            this.findIP.TabIndex = 1;
            this.findIP.Text = "Узнать";
            this.findIP.UseVisualStyleBackColor = true;
            this.findIP.Click += new System.EventHandler(this.findIP_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.findDomain);
            this.groupBox2.Controls.Add(this.ip);
            this.groupBox2.Location = new System.Drawing.Point(177, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(158, 82);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Домен по IP";
            // 
            // findDomain
            // 
            this.findDomain.Location = new System.Drawing.Point(7, 47);
            this.findDomain.Name = "findDomain";
            this.findDomain.Size = new System.Drawing.Size(75, 23);
            this.findDomain.TabIndex = 1;
            this.findDomain.Text = "Узнать";
            this.findDomain.UseVisualStyleBackColor = true;
            this.findDomain.Click += new System.EventHandler(this.findDomain_Click);
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(7, 20);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(140, 20);
            this.ip.TabIndex = 0;
            // 
            // aboutDHCP
            // 
            this.aboutDHCP.Location = new System.Drawing.Point(342, 33);
            this.aboutDHCP.Name = "aboutDHCP";
            this.aboutDHCP.Size = new System.Drawing.Size(75, 23);
            this.aboutDHCP.TabIndex = 3;
            this.aboutDHCP.Text = "DCHP";
            this.aboutDHCP.UseVisualStyleBackColor = true;
            this.aboutDHCP.Click += new System.EventHandler(this.aboutDHCP_Click);
            // 
            // resetIP
            // 
            this.resetIP.Location = new System.Drawing.Point(341, 60);
            this.resetIP.Name = "resetIP";
            this.resetIP.Size = new System.Drawing.Size(75, 23);
            this.resetIP.TabIndex = 4;
            this.resetIP.Text = "Сброс IP";
            this.resetIP.UseVisualStyleBackColor = true;
            this.resetIP.Click += new System.EventHandler(this.resetIP_Click);
            // 
            // resultInfo
            // 
            this.resultInfo.Location = new System.Drawing.Point(13, 102);
            this.resultInfo.Name = "resultInfo";
            this.resultInfo.Size = new System.Drawing.Size(403, 117);
            this.resultInfo.TabIndex = 5;
            this.resultInfo.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 232);
            this.Controls.Add(this.resultInfo);
            this.Controls.Add(this.resetIP);
            this.Controls.Add(this.aboutDHCP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "DHCP & DNS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button findIP;
        private System.Windows.Forms.TextBox domain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button findDomain;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.Button aboutDHCP;
        private System.Windows.Forms.Button resetIP;
        private System.Windows.Forms.RichTextBox resultInfo;
    }
}

