namespace OS_lab_3_2_1
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
			this.createSemaphore = new System.Windows.Forms.Button();
			this.connectSemaphore = new System.Windows.Forms.Button();
			this.counter = new System.Windows.Forms.TextBox();
			this.closeSemaphore = new System.Windows.Forms.Button();
			this.runTimer = new System.Windows.Forms.Timer(this.components);
			this.stopTimer = new System.Windows.Forms.Timer(this.components);
			this.amountSemaphores = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.amountSemaphores)).BeginInit();
			this.SuspendLayout();
			// 
			// createSemaphore
			// 
			this.createSemaphore.Location = new System.Drawing.Point(13, 13);
			this.createSemaphore.Name = "createSemaphore";
			this.createSemaphore.Size = new System.Drawing.Size(198, 23);
			this.createSemaphore.TabIndex = 0;
			this.createSemaphore.Text = "Создать семафор";
			this.createSemaphore.UseVisualStyleBackColor = true;
			this.createSemaphore.Click += new System.EventHandler(this.createSemaphore_Click);
			// 
			// connectSemaphore
			// 
			this.connectSemaphore.Location = new System.Drawing.Point(13, 42);
			this.connectSemaphore.Name = "connectSemaphore";
			this.connectSemaphore.Size = new System.Drawing.Size(267, 23);
			this.connectSemaphore.TabIndex = 1;
			this.connectSemaphore.Text = "Подключиться к семафору";
			this.connectSemaphore.UseVisualStyleBackColor = true;
			this.connectSemaphore.Click += new System.EventHandler(this.connectSemaphore_Click);
			// 
			// counter
			// 
			this.counter.Location = new System.Drawing.Point(13, 72);
			this.counter.Name = "counter";
			this.counter.ReadOnly = true;
			this.counter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.counter.Size = new System.Drawing.Size(267, 20);
			this.counter.TabIndex = 2;
			this.counter.Text = "0";
			// 
			// closeSemaphore
			// 
			this.closeSemaphore.Location = new System.Drawing.Point(13, 98);
			this.closeSemaphore.Name = "closeSemaphore";
			this.closeSemaphore.Size = new System.Drawing.Size(267, 23);
			this.closeSemaphore.TabIndex = 3;
			this.closeSemaphore.Text = "Закрыть семафор";
			this.closeSemaphore.UseVisualStyleBackColor = true;
			this.closeSemaphore.Click += new System.EventHandler(this.disconnectSemaphore_Click);
			// 
			// runTimer
			// 
			this.runTimer.Tick += new System.EventHandler(this.runTimer_Tick);
			// 
			// stopTimer
			// 
			this.stopTimer.Interval = 500;
			this.stopTimer.Tick += new System.EventHandler(this.stopTimer_Tick);
			// 
			// amountSemaphores
			// 
			this.amountSemaphores.Location = new System.Drawing.Point(217, 15);
			this.amountSemaphores.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.amountSemaphores.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.amountSemaphores.Name = "amountSemaphores";
			this.amountSemaphores.Size = new System.Drawing.Size(63, 20);
			this.amountSemaphores.TabIndex = 4;
			this.amountSemaphores.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 134);
			this.Controls.Add(this.amountSemaphores);
			this.Controls.Add(this.closeSemaphore);
			this.Controls.Add(this.counter);
			this.Controls.Add(this.connectSemaphore);
			this.Controls.Add(this.createSemaphore);
			this.Name = "MainForm";
			this.Text = "Семафоры";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.amountSemaphores)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createSemaphore;
        private System.Windows.Forms.Button connectSemaphore;
        private System.Windows.Forms.TextBox counter;
        private System.Windows.Forms.Button closeSemaphore;
        private System.Windows.Forms.Timer runTimer;
        private System.Windows.Forms.Timer stopTimer;
        private System.Windows.Forms.NumericUpDown amountSemaphores;
    }
}

