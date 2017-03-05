namespace Tester
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
            this.ctrlFloatNumbers1 = new ctrlFloatNumbersLib.ctrlFloatNumbers();
            this.SuspendLayout();
            // 
            // ctrlFloatNumbers1
            // 
            this.ctrlFloatNumbers1.Fractional = 0D;
            this.ctrlFloatNumbers1.Integer = 0;
            this.ctrlFloatNumbers1.Location = new System.Drawing.Point(61, 52);
            this.ctrlFloatNumbers1.MaxValue = 1.7976931348623157E+308D;
            this.ctrlFloatNumbers1.MinValue = -1.7976931348623157E+308D;
            this.ctrlFloatNumbers1.Name = "ctrlFloatNumbers1";
            this.ctrlFloatNumbers1.Size = new System.Drawing.Size(150, 150);
            this.ctrlFloatNumbers1.StepDown = 1D;
            this.ctrlFloatNumbers1.StepUp = 1D;
            this.ctrlFloatNumbers1.String = "0";
            this.ctrlFloatNumbers1.TabIndex = 0;
            this.ctrlFloatNumbers1.Value = 0D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ctrlFloatNumbers1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlFloatNumbersLib.ctrlFloatNumbers ctrlFloatNumbers1;

    }
}

