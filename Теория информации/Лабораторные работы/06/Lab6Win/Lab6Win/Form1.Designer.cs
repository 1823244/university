namespace Lab6Win
{
    partial class Form1
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
            this.inf1T = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.codingBtn = new System.Windows.Forms.Button();
            this.code1T = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.confirmErrorBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.codeErrT = new System.Windows.Forms.TextBox();
            this.isprBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.syndrT = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.codeIsprT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.inf2T = new System.Windows.Forms.TextBox();
            this.cleanAllBtn = new System.Windows.Forms.Button();
            this.decodingBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // inf1T
            // 
            this.inf1T.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.inf1T.Location = new System.Drawing.Point(474, 154);
            this.inf1T.Name = "inf1T";
            this.inf1T.Size = new System.Drawing.Size(222, 39);
            this.inf1T.TabIndex = 0;
            this.inf1T.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inf1T_KeyPress);
            this.inf1T.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inf1T_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(469, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Информационное слово";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(469, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Кодовое слово";
            // 
            // codingBtn
            // 
            this.codingBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.codingBtn.Location = new System.Drawing.Point(722, 154);
            this.codingBtn.Name = "codingBtn";
            this.codingBtn.Size = new System.Drawing.Size(201, 40);
            this.codingBtn.TabIndex = 3;
            this.codingBtn.Text = "Кодировать";
            this.codingBtn.UseVisualStyleBackColor = true;
            this.codingBtn.Click += new System.EventHandler(this.codingBtn_Click);
            // 
            // code1T
            // 
            this.code1T.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.code1T.Location = new System.Drawing.Point(474, 239);
            this.code1T.Name = "code1T";
            this.code1T.ReadOnly = true;
            this.code1T.Size = new System.Drawing.Size(222, 39);
            this.code1T.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(471, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Сделать ошибку в";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numericUpDown1.Location = new System.Drawing.Point(617, 301);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(40, 29);
            this.numericUpDown1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(663, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "бите";
            // 
            // confirmErrorBtn
            // 
            this.confirmErrorBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.confirmErrorBtn.Location = new System.Drawing.Point(722, 290);
            this.confirmErrorBtn.Name = "confirmErrorBtn";
            this.confirmErrorBtn.Size = new System.Drawing.Size(90, 40);
            this.confirmErrorBtn.TabIndex = 8;
            this.confirmErrorBtn.Text = "OK";
            this.confirmErrorBtn.UseVisualStyleBackColor = true;
            this.confirmErrorBtn.Click += new System.EventHandler(this.confirmErrorBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(469, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Кодовое слово с ошибкой";
            // 
            // codeErrT
            // 
            this.codeErrT.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.codeErrT.Location = new System.Drawing.Point(474, 372);
            this.codeErrT.Name = "codeErrT";
            this.codeErrT.ReadOnly = true;
            this.codeErrT.Size = new System.Drawing.Size(222, 39);
            this.codeErrT.TabIndex = 10;
            // 
            // isprBtn
            // 
            this.isprBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.isprBtn.Location = new System.Drawing.Point(722, 372);
            this.isprBtn.Name = "isprBtn";
            this.isprBtn.Size = new System.Drawing.Size(201, 40);
            this.isprBtn.TabIndex = 11;
            this.isprBtn.Text = "Исправить";
            this.isprBtn.UseVisualStyleBackColor = true;
            this.isprBtn.Click += new System.EventHandler(this.isprBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(470, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Синдром";
            // 
            // syndrT
            // 
            this.syndrT.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.syndrT.Location = new System.Drawing.Point(474, 462);
            this.syndrT.Name = "syndrT";
            this.syndrT.ReadOnly = true;
            this.syndrT.Size = new System.Drawing.Size(222, 39);
            this.syndrT.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(470, 522);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(249, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "Кодовое слово без ошибки";
            // 
            // codeIsprT
            // 
            this.codeIsprT.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.codeIsprT.Location = new System.Drawing.Point(474, 550);
            this.codeIsprT.Name = "codeIsprT";
            this.codeIsprT.ReadOnly = true;
            this.codeIsprT.Size = new System.Drawing.Size(222, 39);
            this.codeIsprT.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(469, 616);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(227, 25);
            this.label8.TabIndex = 16;
            this.label8.Text = "Информационное слово";
            // 
            // inf2T
            // 
            this.inf2T.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.inf2T.Location = new System.Drawing.Point(474, 644);
            this.inf2T.Name = "inf2T";
            this.inf2T.ReadOnly = true;
            this.inf2T.Size = new System.Drawing.Size(222, 39);
            this.inf2T.TabIndex = 17;
            // 
            // cleanAllBtn
            // 
            this.cleanAllBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.cleanAllBtn.Location = new System.Drawing.Point(1048, 716);
            this.cleanAllBtn.Name = "cleanAllBtn";
            this.cleanAllBtn.Size = new System.Drawing.Size(150, 40);
            this.cleanAllBtn.TabIndex = 18;
            this.cleanAllBtn.Text = "Заново";
            this.cleanAllBtn.UseVisualStyleBackColor = true;
            this.cleanAllBtn.Click += new System.EventHandler(this.cleanAllBtn_Click);
            // 
            // decodingBtn
            // 
            this.decodingBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.decodingBtn.Location = new System.Drawing.Point(722, 550);
            this.decodingBtn.Name = "decodingBtn";
            this.decodingBtn.Size = new System.Drawing.Size(201, 40);
            this.decodingBtn.TabIndex = 19;
            this.decodingBtn.Text = "Декодировать";
            this.decodingBtn.UseVisualStyleBackColor = true;
            this.decodingBtn.Click += new System.EventHandler(this.decodingBtn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.button1.Location = new System.Drawing.Point(1204, 716);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 20;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.decodingBtn);
            this.Controls.Add(this.cleanAllBtn);
            this.Controls.Add(this.inf2T);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.codeIsprT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.syndrT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.isprBtn);
            this.Controls.Add(this.codeErrT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.confirmErrorBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.code1T);
            this.Controls.Add(this.codingBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inf1T);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inf1T;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button codingBtn;
        private System.Windows.Forms.TextBox code1T;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button confirmErrorBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox codeErrT;
        private System.Windows.Forms.Button isprBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox syndrT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox codeIsprT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox inf2T;
        private System.Windows.Forms.Button cleanAllBtn;
        private System.Windows.Forms.Button decodingBtn;
        private System.Windows.Forms.Button button1;
    }
}

