namespace app
{
    partial class Alerts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alerts));
            this.alertsTable = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.alertsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // alertsTable
            // 
            this.alertsTable.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.alertsTable.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.alertsTable.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.alertsTable.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.alertsTable.ColumnInfo = resources.GetString("alertsTable.ColumnInfo");
            this.alertsTable.Enabled = false;
            this.alertsTable.ExtendLastCol = true;
            this.alertsTable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.alertsTable.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.alertsTable.Location = new System.Drawing.Point(12, 12);
            this.alertsTable.Name = "alertsTable";
            this.alertsTable.Rows.DefaultSize = 24;
            this.alertsTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.alertsTable.Size = new System.Drawing.Size(460, 237);
            this.alertsTable.StyleInfo = resources.GetString("alertsTable.StyleInfo");
            this.alertsTable.TabIndex = 0;
            // 
            // Alerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.alertsTable);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Alerts";
            this.Text = "Alerts";
            this.Load += new System.EventHandler(this.Alerts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alertsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid alertsTable;
    }
}