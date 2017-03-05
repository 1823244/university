namespace OS_lab3
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
			this.components = new System.ComponentModel.Container();
			this.timeSleep = new System.Windows.Forms.TextBox();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.timeSleepLabel = new System.Windows.Forms.Label();
			this.timeSleepLabelAfter = new System.Windows.Forms.Label();
			this.programIdleLabel = new System.Windows.Forms.Label();
			this.programIdle = new System.Windows.Forms.TextBox();
			this.programIdleLabelAfter = new System.Windows.Forms.Label();
			this.errors = new System.Windows.Forms.Label();
			this.letsGo = new System.Windows.Forms.Button();
			this.processNameLabel = new System.Windows.Forms.Label();
			this.processName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// timeSleep
			// 
			this.timeSleep.Location = new System.Drawing.Point(129, 41);
			this.timeSleep.Name = "timeSleep";
			this.timeSleep.Size = new System.Drawing.Size(65, 20);
			this.timeSleep.TabIndex = 0;
			this.timeSleep.Text = "1";
			// 
			// mainTimer
			// 
			this.mainTimer.Interval = 1000;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// timeSleepLabel
			// 
			this.timeSleepLabel.AutoSize = true;
			this.timeSleepLabel.Location = new System.Drawing.Point(12, 44);
			this.timeSleepLabel.Name = "timeSleepLabel";
			this.timeSleepLabel.Size = new System.Drawing.Size(111, 13);
			this.timeSleepLabel.TabIndex = 1;
			this.timeSleepLabel.Text = "Ожидать программу";
			// 
			// timeSleepLabelAfter
			// 
			this.timeSleepLabelAfter.AutoSize = true;
			this.timeSleepLabelAfter.Location = new System.Drawing.Point(201, 44);
			this.timeSleepLabelAfter.Name = "timeSleepLabelAfter";
			this.timeSleepLabelAfter.Size = new System.Drawing.Size(13, 13);
			this.timeSleepLabelAfter.TabIndex = 2;
			this.timeSleepLabelAfter.Text = "с";
			// 
			// programIdleLabel
			// 
			this.programIdleLabel.AutoSize = true;
			this.programIdleLabel.Location = new System.Drawing.Point(12, 78);
			this.programIdleLabel.Name = "programIdleLabel";
			this.programIdleLabel.Size = new System.Drawing.Size(149, 13);
			this.programIdleLabel.TabIndex = 3;
			this.programIdleLabel.Text = "Время простоя программы:";
			// 
			// programIdle
			// 
			this.programIdle.Location = new System.Drawing.Point(168, 75);
			this.programIdle.Name = "programIdle";
			this.programIdle.ReadOnly = true;
			this.programIdle.Size = new System.Drawing.Size(53, 20);
			this.programIdle.TabIndex = 4;
			// 
			// programIdleLabelAfter
			// 
			this.programIdleLabelAfter.AutoSize = true;
			this.programIdleLabelAfter.Location = new System.Drawing.Point(227, 78);
			this.programIdleLabelAfter.Name = "programIdleLabelAfter";
			this.programIdleLabelAfter.Size = new System.Drawing.Size(21, 13);
			this.programIdleLabelAfter.TabIndex = 5;
			this.programIdleLabelAfter.Text = "мс";
			// 
			// errors
			// 
			this.errors.AutoSize = true;
			this.errors.Location = new System.Drawing.Point(12, 110);
			this.errors.Name = "errors";
			this.errors.Size = new System.Drawing.Size(0, 13);
			this.errors.TabIndex = 7;
			// 
			// letsGo
			// 
			this.letsGo.Location = new System.Drawing.Point(240, 39);
			this.letsGo.Name = "letsGo";
			this.letsGo.Size = new System.Drawing.Size(75, 23);
			this.letsGo.TabIndex = 8;
			this.letsGo.Text = "Запустить";
			this.letsGo.UseVisualStyleBackColor = true;
			this.letsGo.Click += new System.EventHandler(this.letsGo_Click);
			// 
			// processNameLabel
			// 
			this.processNameLabel.AutoSize = true;
			this.processNameLabel.Location = new System.Drawing.Point(13, 13);
			this.processNameLabel.Name = "processNameLabel";
			this.processNameLabel.Size = new System.Drawing.Size(86, 13);
			this.processNameLabel.TabIndex = 9;
			this.processNameLabel.Text = "Имя процесса: ";
			// 
			// processName
			// 
			this.processName.Location = new System.Drawing.Point(105, 10);
			this.processName.Name = "processName";
			this.processName.Size = new System.Drawing.Size(210, 20);
			this.processName.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(328, 148);
			this.Controls.Add(this.processName);
			this.Controls.Add(this.processNameLabel);
			this.Controls.Add(this.letsGo);
			this.Controls.Add(this.errors);
			this.Controls.Add(this.programIdleLabelAfter);
			this.Controls.Add(this.programIdle);
			this.Controls.Add(this.programIdleLabel);
			this.Controls.Add(this.timeSleepLabelAfter);
			this.Controls.Add(this.timeSleepLabel);
			this.Controls.Add(this.timeSleep);
			this.Name = "MainForm";
			this.Text = "Ожидание";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeSleep;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Label timeSleepLabel;
        private System.Windows.Forms.Label timeSleepLabelAfter;
        private System.Windows.Forms.Label programIdleLabel;
        private System.Windows.Forms.TextBox programIdle;
		private System.Windows.Forms.Label programIdleLabelAfter;
        private System.Windows.Forms.Label errors;
        private System.Windows.Forms.Button letsGo;
        private System.Windows.Forms.Label processNameLabel;
        private System.Windows.Forms.TextBox processName;
    }
}

