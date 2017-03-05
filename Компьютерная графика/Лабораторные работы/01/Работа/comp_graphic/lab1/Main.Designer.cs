namespace lab1
{
    partial class Main
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.squaresNumUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.squaresNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // squaresNumUpDown
            // 
            this.squaresNumUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.squaresNumUpDown.Location = new System.Drawing.Point(123, 123);
            this.squaresNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.squaresNumUpDown.Name = "squaresNumUpDown";
            this.squaresNumUpDown.Size = new System.Drawing.Size(49, 20);
            this.squaresNumUpDown.TabIndex = 0;
            this.squaresNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.squaresNumUpDown.ValueChanged += new System.EventHandler(this.squaresNumUpDown_ValueChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.squaresNumUpDown);
            this.Name = "Main";
            this.Text = "Lab 1";
            ((System.ComponentModel.ISupportInitialize)(this.squaresNumUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NumericUpDown squaresNumUpDown;
    }
}

