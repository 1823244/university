﻿namespace Lab3
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
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.radioServer = new System.Windows.Forms.RadioButton();
            this.radioClient = new System.Windows.Forms.RadioButton();
            this.groupProtocol = new System.Windows.Forms.GroupBox();
            this.radioSPX = new System.Windows.Forms.RadioButton();
            this.radioIPX = new System.Windows.Forms.RadioButton();
            this.groupSocket = new System.Windows.Forms.GroupBox();
            this.textSocket = new System.Windows.Forms.TextBox();
            this.groupMAC = new System.Windows.Forms.GroupBox();
            this.textMAC = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textLocalMAC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupMode.SuspendLayout();
            this.groupProtocol.SuspendLayout();
            this.groupSocket.SuspendLayout();
            this.groupMAC.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioServer);
            this.groupMode.Controls.Add(this.radioClient);
            this.groupMode.Location = new System.Drawing.Point(14, 12);
            this.groupMode.Name = "groupMode";
            this.groupMode.Size = new System.Drawing.Size(126, 53);
            this.groupMode.TabIndex = 2;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Режим";
            // 
            // radioServer
            // 
            this.radioServer.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioServer.AutoSize = true;
            this.radioServer.Location = new System.Drawing.Point(65, 19);
            this.radioServer.Name = "radioServer";
            this.radioServer.Size = new System.Drawing.Size(54, 23);
            this.radioServer.TabIndex = 2;
            this.radioServer.Text = "Сервер";
            this.radioServer.UseVisualStyleBackColor = true;
            // 
            // radioClient
            // 
            this.radioClient.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioClient.AutoSize = true;
            this.radioClient.Checked = true;
            this.radioClient.Location = new System.Drawing.Point(6, 19);
            this.radioClient.Name = "radioClient";
            this.radioClient.Size = new System.Drawing.Size(53, 23);
            this.radioClient.TabIndex = 1;
            this.radioClient.TabStop = true;
            this.radioClient.Text = "Клиент";
            this.radioClient.UseVisualStyleBackColor = true;
            this.radioClient.CheckedChanged += new System.EventHandler(this.radioClient_CheckedChanged);
            // 
            // groupProtocol
            // 
            this.groupProtocol.Controls.Add(this.radioSPX);
            this.groupProtocol.Controls.Add(this.radioIPX);
            this.groupProtocol.Location = new System.Drawing.Point(146, 13);
            this.groupProtocol.Name = "groupProtocol";
            this.groupProtocol.Size = new System.Drawing.Size(92, 52);
            this.groupProtocol.TabIndex = 3;
            this.groupProtocol.TabStop = false;
            this.groupProtocol.Text = "Протокол";
            // 
            // radioSPX
            // 
            this.radioSPX.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioSPX.AutoSize = true;
            this.radioSPX.Location = new System.Drawing.Point(47, 20);
            this.radioSPX.Name = "radioSPX";
            this.radioSPX.Size = new System.Drawing.Size(38, 23);
            this.radioSPX.TabIndex = 1;
            this.radioSPX.Text = "SPX";
            this.radioSPX.UseVisualStyleBackColor = true;
            // 
            // radioIPX
            // 
            this.radioIPX.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioIPX.AutoSize = true;
            this.radioIPX.Checked = true;
            this.radioIPX.Location = new System.Drawing.Point(7, 20);
            this.radioIPX.Name = "radioIPX";
            this.radioIPX.Size = new System.Drawing.Size(34, 23);
            this.radioIPX.TabIndex = 0;
            this.radioIPX.TabStop = true;
            this.radioIPX.Text = "IPX";
            this.radioIPX.UseVisualStyleBackColor = true;
            this.radioIPX.CheckedChanged += new System.EventHandler(this.radioIPX_CheckedChanged);
            // 
            // groupSocket
            // 
            this.groupSocket.Controls.Add(this.textSocket);
            this.groupSocket.Location = new System.Drawing.Point(245, 13);
            this.groupSocket.Name = "groupSocket";
            this.groupSocket.Size = new System.Drawing.Size(59, 52);
            this.groupSocket.TabIndex = 4;
            this.groupSocket.TabStop = false;
            this.groupSocket.Text = "Сокет";
            // 
            // textSocket
            // 
            this.textSocket.Location = new System.Drawing.Point(6, 22);
            this.textSocket.MaxLength = 6;
            this.textSocket.Name = "textSocket";
            this.textSocket.Size = new System.Drawing.Size(46, 20);
            this.textSocket.TabIndex = 0;
            this.textSocket.Text = "1500";
            this.textSocket.TextChanged += new System.EventHandler(this.textSocket_TextChanged);
            // 
            // groupMAC
            // 
            this.groupMAC.Controls.Add(this.textMAC);
            this.groupMAC.Location = new System.Drawing.Point(311, 13);
            this.groupMAC.Name = "groupMAC";
            this.groupMAC.Size = new System.Drawing.Size(101, 52);
            this.groupMAC.TabIndex = 5;
            this.groupMAC.TabStop = false;
            this.groupMAC.Text = "Адрес сервера";
            // 
            // textMAC
            // 
            this.textMAC.Location = new System.Drawing.Point(7, 22);
            this.textMAC.MaxLength = 12;
            this.textMAC.Name = "textMAC";
            this.textMAC.ReadOnly = true;
            this.textMAC.Size = new System.Drawing.Size(88, 20);
            this.textMAC.TabIndex = 0;
            this.textMAC.Text = "FFFFFFFFFFFF";
            this.textMAC.TextChanged += new System.EventHandler(this.textMAC_TextChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(419, 14);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(53, 52);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Старт!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 72);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(457, 31);
            this.progressBar.TabIndex = 8;
            // 
            // statusValue
            // 
            this.statusValue.Margin = new System.Windows.Forms.Padding(12, 3, 0, 2);
            this.statusValue.Name = "statusValue";
            this.statusValue.Size = new System.Drawing.Size(0, 17);
            this.statusValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusValue,
            this.statusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 110);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(487, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textLocalMAC
            // 
            this.textLocalMAC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLocalMAC.Location = new System.Drawing.Point(392, 113);
            this.textLocalMAC.Multiline = true;
            this.textLocalMAC.Name = "textLocalMAC";
            this.textLocalMAC.ReadOnly = true;
            this.textLocalMAC.Size = new System.Drawing.Size(80, 18);
            this.textLocalMAC.TabIndex = 9;
            this.textLocalMAC.Text = "08002798F0FA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Адрес:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 132);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textLocalMAC);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupMAC);
            this.Controls.Add(this.groupSocket);
            this.Controls.Add(this.groupProtocol);
            this.Controls.Add(this.groupMode);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "IPX & SPX";
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            this.groupProtocol.ResumeLayout(false);
            this.groupProtocol.PerformLayout();
            this.groupSocket.ResumeLayout(false);
            this.groupSocket.PerformLayout();
            this.groupMAC.ResumeLayout(false);
            this.groupMAC.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.RadioButton radioServer;
        private System.Windows.Forms.RadioButton radioClient;
        private System.Windows.Forms.GroupBox groupProtocol;
        private System.Windows.Forms.RadioButton radioSPX;
        private System.Windows.Forms.RadioButton radioIPX;
        private System.Windows.Forms.GroupBox groupSocket;
        private System.Windows.Forms.GroupBox groupMAC;
        private System.Windows.Forms.TextBox textMAC;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textSocket;
        private System.Windows.Forms.ToolStripStatusLabel statusValue;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textLocalMAC;
        private System.Windows.Forms.Label label1;
    }
}

