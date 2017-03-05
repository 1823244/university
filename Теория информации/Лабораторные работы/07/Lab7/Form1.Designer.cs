namespace Lab7
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numerErr = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb4 = new System.Windows.Forms.TextBox();
            this.tb5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCod = new System.Windows.Forms.Button();
            this.btnDecod = new System.Windows.Forms.Button();
            this.btnErr = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb6 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numerErr)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(410, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Информационное слово";
            // 
            // tb1
            // 
            this.tb1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb1.Location = new System.Drawing.Point(416, 112);
            this.tb1.MaxLength = 4;
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(330, 39);
            this.tb1.TabIndex = 1;
            this.tb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb1_KeyPress);
            this.tb1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb1_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(410, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Кодовое слово";
            // 
            // tb2
            // 
            this.tb2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb2.Location = new System.Drawing.Point(416, 209);
            this.tb2.Name = "tb2";
            this.tb2.ReadOnly = true;
            this.tb2.Size = new System.Drawing.Size(330, 39);
            this.tb2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(410, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ошибка при степени";
            // 
            // numerErr
            // 
            this.numerErr.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.numerErr.Location = new System.Drawing.Point(649, 268);
            this.numerErr.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numerErr.Name = "numerErr";
            this.numerErr.Size = new System.Drawing.Size(42, 38);
            this.numerErr.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(410, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "Кодовое слово с ошибкой";
            // 
            // tb3
            // 
            this.tb3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb3.Location = new System.Drawing.Point(416, 366);
            this.tb3.Name = "tb3";
            this.tb3.ReadOnly = true;
            this.tb3.Size = new System.Drawing.Size(330, 39);
            this.tb3.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(410, 425);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 31);
            this.label5.TabIndex = 8;
            this.label5.Text = "Синдром";
            // 
            // tb4
            // 
            this.tb4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb4.Location = new System.Drawing.Point(416, 459);
            this.tb4.Name = "tb4";
            this.tb4.ReadOnly = true;
            this.tb4.Size = new System.Drawing.Size(330, 39);
            this.tb4.TabIndex = 9;
            // 
            // tb5
            // 
            this.tb5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb5.Location = new System.Drawing.Point(416, 596);
            this.tb5.Name = "tb5";
            this.tb5.ReadOnly = true;
            this.tb5.Size = new System.Drawing.Size(330, 39);
            this.tb5.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(410, 520);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(329, 31);
            this.label6.TabIndex = 11;
            this.label6.Text = "После исправления ошибки";
            // 
            // btnCod
            // 
            this.btnCod.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.btnCod.Location = new System.Drawing.Point(776, 112);
            this.btnCod.Name = "btnCod";
            this.btnCod.Size = new System.Drawing.Size(201, 39);
            this.btnCod.TabIndex = 12;
            this.btnCod.Text = "Кодировать";
            this.btnCod.UseVisualStyleBackColor = true;
            this.btnCod.Click += new System.EventHandler(this.btnCod_Click);
            // 
            // btnDecod
            // 
            this.btnDecod.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.btnDecod.Location = new System.Drawing.Point(776, 366);
            this.btnDecod.Name = "btnDecod";
            this.btnDecod.Size = new System.Drawing.Size(201, 39);
            this.btnDecod.TabIndex = 13;
            this.btnDecod.Text = "Декодировать";
            this.btnDecod.UseVisualStyleBackColor = true;
            this.btnDecod.Click += new System.EventHandler(this.btnDecod_Click);
            // 
            // btnErr
            // 
            this.btnErr.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.btnErr.Location = new System.Drawing.Point(776, 264);
            this.btnErr.Name = "btnErr";
            this.btnErr.Size = new System.Drawing.Size(201, 39);
            this.btnErr.TabIndex = 14;
            this.btnErr.Text = "Допустить ошибку";
            this.btnErr.UseVisualStyleBackColor = true;
            this.btnErr.Click += new System.EventHandler(this.btnErr_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(410, 562);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 31);
            this.label7.TabIndex = 15;
            this.label7.Text = "Кодовое слово";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(410, 654);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 31);
            this.label8.TabIndex = 16;
            this.label8.Text = "Исходное слово";
            // 
            // tb6
            // 
            this.tb6.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.tb6.Location = new System.Drawing.Point(416, 688);
            this.tb6.Name = "tb6";
            this.tb6.ReadOnly = true;
            this.tb6.Size = new System.Drawing.Size(330, 39);
            this.tb6.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.button1.Location = new System.Drawing.Point(1213, 717);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 39);
            this.button1.TabIndex = 18;
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
            this.Controls.Add(this.tb6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnErr);
            this.Controls.Add(this.btnDecod);
            this.Controls.Add(this.btnCod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb5);
            this.Controls.Add(this.tb4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numerErr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numerErr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numerErr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb4;
        private System.Windows.Forms.TextBox tb5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCod;
        private System.Windows.Forms.Button btnDecod;
        private System.Windows.Forms.Button btnErr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb6;
        private System.Windows.Forms.Button button1;
    }
}

