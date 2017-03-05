namespace lab2
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
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.numericUpDownMin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMax = new System.Windows.Forms.NumericUpDown();
            this.chart = new Chart.Chart();
            this.labelMinValue = new System.Windows.Forms.Label();
            this.labelMaxValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(13, 13);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(26, 13);
            this.labelMin.TabIndex = 2;
            this.labelMin.Text = "min:";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(13, 61);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(29, 13);
            this.labelMax.TabIndex = 3;
            this.labelMax.Text = "max:";
            // 
            // numericUpDownMin
            // 
            this.numericUpDownMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMin.Location = new System.Drawing.Point(16, 29);
            this.numericUpDownMin.Name = "numericUpDownMin";
            this.numericUpDownMin.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownMin.TabIndex = 5;
            this.numericUpDownMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownMin_KeyDown);
            // 
            // numericUpDownMax
            // 
            this.numericUpDownMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMax.Location = new System.Drawing.Point(16, 78);
            this.numericUpDownMax.Name = "numericUpDownMax";
            this.numericUpDownMax.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownMax.TabIndex = 6;
            this.numericUpDownMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownMax_KeyDown);
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.Color = System.Drawing.Color.Black;
            this.chart.Location = new System.Drawing.Point(91, 12);
            this.chart.Max = 11F;
            this.chart.Min = -9F;
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(390, 308);
            this.chart.Step = 50;
            this.chart.TabIndex = 0;
            // 
            // labelMinValue
            // 
            this.labelMinValue.AutoSize = true;
            this.labelMinValue.Location = new System.Drawing.Point(45, 13);
            this.labelMinValue.Name = "labelMinValue";
            this.labelMinValue.Size = new System.Drawing.Size(13, 13);
            this.labelMinValue.TabIndex = 7;
            this.labelMinValue.Text = "0";
            // 
            // labelMaxValue
            // 
            this.labelMaxValue.AutoSize = true;
            this.labelMaxValue.Location = new System.Drawing.Point(45, 62);
            this.labelMaxValue.Name = "labelMaxValue";
            this.labelMaxValue.Size = new System.Drawing.Size(13, 13);
            this.labelMaxValue.TabIndex = 8;
            this.labelMaxValue.Text = "0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 332);
            this.Controls.Add(this.labelMaxValue);
            this.Controls.Add(this.labelMinValue);
            this.Controls.Add(this.numericUpDownMax);
            this.Controls.Add(this.numericUpDownMin);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.chart);
            this.Name = "Main";
            this.Text = "lab2";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Chart.Chart chart;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.NumericUpDown numericUpDownMin;
        private System.Windows.Forms.NumericUpDown numericUpDownMax;
        private System.Windows.Forms.Label labelMinValue;
        private System.Windows.Forms.Label labelMaxValue;
    }
}

