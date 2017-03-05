namespace app
{
    partial class Trains
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trains));
            this.currentTime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.labelLastTrain = new System.Windows.Forms.Label();
            this.lastTrain = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.violations = new System.Windows.Forms.Button();
            this.board = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ads = new System.Windows.Forms.Label();
            this.fastTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            this.SuspendLayout();
            // 
            // currentTime
            // 
            this.currentTime.AutoSize = true;
            this.currentTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentTime.Location = new System.Drawing.Point(567, 10);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(45, 19);
            this.currentTime.TabIndex = 0;
            this.currentTime.Text = "label1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentTime.Location = new System.Drawing.Point(452, 10);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(109, 19);
            this.labelCurrentTime.TabIndex = 1;
            this.labelCurrentTime.Text = "Текущее время:";
            this.labelCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLastTrain
            // 
            this.labelLastTrain.AutoSize = true;
            this.labelLastTrain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLastTrain.Location = new System.Drawing.Point(12, 10);
            this.labelLastTrain.Name = "labelLastTrain";
            this.labelLastTrain.Size = new System.Drawing.Size(167, 19);
            this.labelLastTrain.TabIndex = 2;
            this.labelLastTrain.Text = "Последний поезд ушел в";
            // 
            // lastTrain
            // 
            this.lastTrain.AutoSize = true;
            this.lastTrain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lastTrain.Location = new System.Drawing.Point(185, 10);
            this.lastTrain.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lastTrain.Name = "lastTrain";
            this.lastTrain.Size = new System.Drawing.Size(45, 19);
            this.lastTrain.TabIndex = 3;
            this.lastTrain.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lastTrain);
            this.panel1.Controls.Add(this.currentTime);
            this.panel1.Controls.Add(this.labelLastTrain);
            this.panel1.Controls.Add(this.labelCurrentTime);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 41);
            this.panel1.TabIndex = 4;
            // 
            // violations
            // 
            this.violations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.violations.Location = new System.Drawing.Point(187, 339);
            this.violations.Name = "violations";
            this.violations.Size = new System.Drawing.Size(250, 30);
            this.violations.TabIndex = 5;
            this.violations.Text = "Показать нарушения расписания";
            this.violations.UseVisualStyleBackColor = true;
            this.violations.Click += new System.EventHandler(this.violations_Click);
            // 
            // board
            // 
            this.board.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.board.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.board.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.board.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.board.ColumnInfo = resources.GetString("board.ColumnInfo");
            this.board.Enabled = false;
            this.board.ExtendLastCol = true;
            this.board.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.board.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.board.Location = new System.Drawing.Point(14, 56);
            this.board.Name = "board";
            this.board.Rows.Count = 8;
            this.board.Rows.DefaultSize = 24;
            this.board.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.board.Size = new System.Drawing.Size(596, 194);
            this.board.StyleInfo = resources.GetString("board.StyleInfo");
            this.board.TabIndex = 8;
            // 
            // ads
            // 
            this.ads.AutoEllipsis = true;
            this.ads.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ads.Location = new System.Drawing.Point(11, 273);
            this.ads.Name = "ads";
            this.ads.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ads.Size = new System.Drawing.Size(600, 30);
            this.ads.TabIndex = 9;
            this.ads.Text = "label1";
            this.ads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fastTimer
            // 
            this.fastTimer.Enabled = true;
            this.fastTimer.Tick += new System.EventHandler(this.fastTimer_Tick);
            // 
            // Trains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.ads);
            this.Controls.Add(this.board);
            this.Controls.Add(this.violations);
            this.Controls.Add(this.panel1);
            this.Name = "Trains";
            this.Text = "  ";
            this.Load += new System.EventHandler(this.Metro_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label currentTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelLastTrain;
        private System.Windows.Forms.Label lastTrain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button violations;
        private C1.Win.C1FlexGrid.C1FlexGrid board;
        private System.Windows.Forms.Label ads;
        private System.Windows.Forms.Timer fastTimer;
    }
}

