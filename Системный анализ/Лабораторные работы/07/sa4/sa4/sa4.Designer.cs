namespace sa4
{
    partial class sa4
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
            this.outbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // outbox
            // 
            this.outbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outbox.Location = new System.Drawing.Point(12, 12);
            this.outbox.Name = "outbox";
            this.outbox.Size = new System.Drawing.Size(1016, 482);
            this.outbox.TabIndex = 3;
            this.outbox.Text = "";
            // 
            // sa4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1035, 506);
            this.Controls.Add(this.outbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "sa4";
            this.Text = "SA#4";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox outbox;


    }
}

