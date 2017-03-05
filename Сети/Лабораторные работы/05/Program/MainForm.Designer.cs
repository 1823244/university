namespace Lab5
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
            this.arpTable = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addARP = new System.Windows.Forms.Button();
            this.addMAC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.delARP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.delIP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.findMACLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.findMAC = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.findIP = new System.Windows.Forms.TextBox();
            this.ARPTableRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // arpTable
            // 
            this.arpTable.Location = new System.Drawing.Point(13, 13);
            this.arpTable.Name = "arpTable";
            this.arpTable.ReadOnly = true;
            this.arpTable.Size = new System.Drawing.Size(611, 143);
            this.arpTable.TabIndex = 0;
            this.arpTable.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addARP);
            this.groupBox1.Controls.Add(this.addMAC);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.addIP);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление";
            // 
            // addARP
            // 
            this.addARP.Location = new System.Drawing.Point(117, 72);
            this.addARP.Name = "addARP";
            this.addARP.Size = new System.Drawing.Size(75, 23);
            this.addARP.TabIndex = 4;
            this.addARP.Text = "Добавить";
            this.addARP.UseVisualStyleBackColor = true;
            this.addARP.Click += new System.EventHandler(this.addARP_Click);
            // 
            // addMAC
            // 
            this.addMAC.Location = new System.Drawing.Point(41, 45);
            this.addMAC.Name = "addMAC";
            this.addMAC.Size = new System.Drawing.Size(152, 20);
            this.addMAC.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MAC:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP:";
            // 
            // addIP
            // 
            this.addIP.Location = new System.Drawing.Point(41, 19);
            this.addIP.Name = "addIP";
            this.addIP.Size = new System.Drawing.Size(152, 20);
            this.addIP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.delARP);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.delIP);
            this.groupBox2.Location = new System.Drawing.Point(218, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Удаление";
            // 
            // delARP
            // 
            this.delARP.Location = new System.Drawing.Point(119, 71);
            this.delARP.Name = "delARP";
            this.delARP.Size = new System.Drawing.Size(75, 23);
            this.delARP.TabIndex = 4;
            this.delARP.Text = "Удалить";
            this.delARP.UseVisualStyleBackColor = true;
            this.delARP.Click += new System.EventHandler(this.delARP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP:";
            // 
            // delIP
            // 
            this.delIP.Location = new System.Drawing.Point(41, 19);
            this.delIP.Name = "delIP";
            this.delIP.Size = new System.Drawing.Size(152, 20);
            this.delIP.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.findMACLabel);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.findMAC);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.findIP);
            this.groupBox3.Location = new System.Drawing.Point(424, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MAC";
            // 
            // findMACLabel
            // 
            this.findMACLabel.AutoSize = true;
            this.findMACLabel.Location = new System.Drawing.Point(38, 48);
            this.findMACLabel.Name = "findMACLabel";
            this.findMACLabel.Size = new System.Drawing.Size(54, 13);
            this.findMACLabel.TabIndex = 8;
            this.findMACLabel.Text = "значение";
            this.findMACLabel.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "MAC:";
            // 
            // findMAC
            // 
            this.findMAC.Location = new System.Drawing.Point(119, 71);
            this.findMAC.Name = "findMAC";
            this.findMAC.Size = new System.Drawing.Size(75, 23);
            this.findMAC.TabIndex = 4;
            this.findMAC.Text = "Узнать";
            this.findMAC.UseVisualStyleBackColor = true;
            this.findMAC.Click += new System.EventHandler(this.findMAC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "IP:";
            // 
            // findIP
            // 
            this.findIP.Location = new System.Drawing.Point(41, 19);
            this.findIP.Name = "findIP";
            this.findIP.Size = new System.Drawing.Size(152, 20);
            this.findIP.TabIndex = 0;
            // 
            // ARPTableRefresh
            // 
            this.ARPTableRefresh.Location = new System.Drawing.Point(549, 133);
            this.ARPTableRefresh.Name = "ARPTableRefresh";
            this.ARPTableRefresh.Size = new System.Drawing.Size(75, 23);
            this.ARPTableRefresh.TabIndex = 7;
            this.ARPTableRefresh.Text = "Обновить";
            this.ARPTableRefresh.UseVisualStyleBackColor = true;
            this.ARPTableRefresh.Click += new System.EventHandler(this.ARPTableRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 274);
            this.Controls.Add(this.ARPTableRefresh);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.arpTable);
            this.Name = "MainForm";
            this.Text = "ARP/RARP";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox arpTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addARP;
        private System.Windows.Forms.TextBox addMAC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button delARP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox delIP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button findMAC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox findIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label findMACLabel;
        private System.Windows.Forms.Button ARPTableRefresh;
    }
}

