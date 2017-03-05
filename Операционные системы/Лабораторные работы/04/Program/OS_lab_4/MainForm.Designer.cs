namespace OS_lab_4
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lbSystemInfo = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.numberOfPagesLabel = new System.Windows.Forms.Label();
			this.numberOfPages = new System.Windows.Forms.NumericUpDown();
			this.btnVirtualFree = new System.Windows.Forms.Button();
			this.btnVirtualAlloc = new System.Windows.Forms.Button();
			this.filePrecent = new System.Windows.Forms.Label();
			this.virtualPercent = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chartVirtual = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.chartPage = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.physPercent = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chartPhys = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.label1 = new System.Windows.Forms.Label();
			this.chartMemoryLoad = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.additionalInfo = new System.Windows.Forms.Label();
			this.mapListBox = new System.Windows.Forms.ListBox();
			this.btnMap = new System.Windows.Forms.Button();
			this.appnameLabel = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numberOfPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartVirtual)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartPage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartPhys)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartMemoryLoad)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(13, 13);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(646, 531);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lbSystemInfo);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(638, 505);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Информация о системе";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lbSystemInfo
			// 
			this.lbSystemInfo.AutoSize = true;
			this.lbSystemInfo.Location = new System.Drawing.Point(6, 3);
			this.lbSystemInfo.Name = "lbSystemInfo";
			this.lbSystemInfo.Size = new System.Drawing.Size(0, 13);
			this.lbSystemInfo.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.numberOfPagesLabel);
			this.tabPage2.Controls.Add(this.numberOfPages);
			this.tabPage2.Controls.Add(this.btnVirtualFree);
			this.tabPage2.Controls.Add(this.btnVirtualAlloc);
			this.tabPage2.Controls.Add(this.filePrecent);
			this.tabPage2.Controls.Add(this.virtualPercent);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.chartVirtual);
			this.tabPage2.Controls.Add(this.chartPage);
			this.tabPage2.Controls.Add(this.physPercent);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.chartPhys);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.chartMemoryLoad);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(638, 505);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Статус памяти";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// numberOfPagesLabel
			// 
			this.numberOfPagesLabel.AutoSize = true;
			this.numberOfPagesLabel.Location = new System.Drawing.Point(124, 478);
			this.numberOfPagesLabel.Name = "numberOfPagesLabel";
			this.numberOfPagesLabel.Size = new System.Drawing.Size(193, 13);
			this.numberOfPagesLabel.TabIndex = 14;
			this.numberOfPagesLabel.Text = "Количество страниц для выделения:";
			// 
			// numberOfPages
			// 
			this.numberOfPages.Location = new System.Drawing.Point(323, 475);
			this.numberOfPages.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numberOfPages.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numberOfPages.Name = "numberOfPages";
			this.numberOfPages.Size = new System.Drawing.Size(80, 20);
			this.numberOfPages.TabIndex = 13;
			this.numberOfPages.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// btnVirtualFree
			// 
			this.btnVirtualFree.Location = new System.Drawing.Point(530, 473);
			this.btnVirtualFree.Name = "btnVirtualFree";
			this.btnVirtualFree.Size = new System.Drawing.Size(96, 23);
			this.btnVirtualFree.TabIndex = 12;
			this.btnVirtualFree.Text = "Освободить";
			this.btnVirtualFree.UseVisualStyleBackColor = true;
			this.btnVirtualFree.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// btnVirtualAlloc
			// 
			this.btnVirtualAlloc.Location = new System.Drawing.Point(409, 473);
			this.btnVirtualAlloc.Name = "btnVirtualAlloc";
			this.btnVirtualAlloc.Size = new System.Drawing.Size(115, 23);
			this.btnVirtualAlloc.TabIndex = 11;
			this.btnVirtualAlloc.Text = "Зарезервировать";
			this.btnVirtualAlloc.UseVisualStyleBackColor = true;
			this.btnVirtualAlloc.Click += new System.EventHandler(this.btnVirtualAlloc_Click_1);
			// 
			// filePrecent
			// 
			this.filePrecent.AutoSize = true;
			this.filePrecent.Location = new System.Drawing.Point(426, 11);
			this.filePrecent.Name = "filePrecent";
			this.filePrecent.Size = new System.Drawing.Size(0, 13);
			this.filePrecent.TabIndex = 10;
			// 
			// virtualPercent
			// 
			this.virtualPercent.AutoSize = true;
			this.virtualPercent.Location = new System.Drawing.Point(320, 231);
			this.virtualPercent.Name = "virtualPercent";
			this.virtualPercent.Size = new System.Drawing.Size(0, 13);
			this.virtualPercent.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(320, 214);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(196, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Виртуальное адресное пространство";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(334, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Файл подкачки";
			// 
			// chartVirtual
			// 
			chartArea1.Name = "ChartArea1";
			this.chartVirtual.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chartVirtual.Legends.Add(legend1);
			this.chartVirtual.Location = new System.Drawing.Point(323, 247);
			this.chartVirtual.Name = "chartVirtual";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series1.IsValueShownAsLabel = true;
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.chartVirtual.Series.Add(series1);
			this.chartVirtual.Size = new System.Drawing.Size(304, 221);
			this.chartVirtual.TabIndex = 6;
			this.chartVirtual.Text = "chart2";
			// 
			// chartPage
			// 
			chartArea2.Name = "ChartArea1";
			this.chartPage.ChartAreas.Add(chartArea2);
			legend2.Name = "Legend1";
			this.chartPage.Legends.Add(legend2);
			this.chartPage.Location = new System.Drawing.Point(323, 30);
			this.chartPage.Name = "chartPage";
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series2.IsValueShownAsLabel = true;
			series2.Legend = "Legend1";
			series2.Name = "Series1";
			this.chartPage.Series.Add(series2);
			this.chartPage.Size = new System.Drawing.Size(304, 177);
			this.chartPage.TabIndex = 5;
			this.chartPage.Text = "chart1";
			// 
			// physPercent
			// 
			this.physPercent.AutoSize = true;
			this.physPercent.Location = new System.Drawing.Point(7, 231);
			this.physPercent.Name = "physPercent";
			this.physPercent.Size = new System.Drawing.Size(0, 13);
			this.physPercent.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 214);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Физическая память";
			// 
			// chartPhys
			// 
			chartArea3.Name = "ChartArea1";
			this.chartPhys.ChartAreas.Add(chartArea3);
			legend3.Name = "Legend1";
			this.chartPhys.Legends.Add(legend3);
			this.chartPhys.Location = new System.Drawing.Point(10, 247);
			this.chartPhys.Name = "chartPhys";
			series3.ChartArea = "ChartArea1";
			series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series3.IsValueShownAsLabel = true;
			series3.Legend = "Legend1";
			series3.Name = "Series1";
			this.chartPhys.Series.Add(series3);
			this.chartPhys.Size = new System.Drawing.Size(307, 221);
			this.chartPhys.TabIndex = 2;
			this.chartPhys.Text = "chart1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(193, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Процент загруженности физ памяти";
			// 
			// chartMemoryLoad
			// 
			this.chartMemoryLoad.BackColor = System.Drawing.Color.WhiteSmoke;
			this.chartMemoryLoad.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
			this.chartMemoryLoad.BorderlineColor = System.Drawing.Color.WhiteSmoke;
			chartArea4.Name = "ChartArea1";
			this.chartMemoryLoad.ChartAreas.Add(chartArea4);
			legend4.Name = "Legend1";
			this.chartMemoryLoad.Legends.Add(legend4);
			this.chartMemoryLoad.Location = new System.Drawing.Point(7, 30);
			this.chartMemoryLoad.Name = "chartMemoryLoad";
			series4.ChartArea = "ChartArea1";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series4.IsValueShownAsLabel = true;
			series4.Legend = "Legend1";
			series4.Name = "Series1";
			this.chartMemoryLoad.Series.Add(series4);
			this.chartMemoryLoad.Size = new System.Drawing.Size(310, 177);
			this.chartMemoryLoad.TabIndex = 0;
			this.chartMemoryLoad.Text = "chart1";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.additionalInfo);
			this.tabPage3.Controls.Add(this.mapListBox);
			this.tabPage3.Controls.Add(this.btnMap);
			this.tabPage3.Controls.Add(this.appnameLabel);
			this.tabPage3.Controls.Add(this.tbName);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(638, 505);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Карта памяти";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// additionalInfo
			// 
			this.additionalInfo.AutoSize = true;
			this.additionalInfo.Location = new System.Drawing.Point(7, 478);
			this.additionalInfo.Name = "additionalInfo";
			this.additionalInfo.Size = new System.Drawing.Size(0, 13);
			this.additionalInfo.TabIndex = 4;
			// 
			// mapListBox
			// 
			this.mapListBox.FormattingEnabled = true;
			this.mapListBox.Location = new System.Drawing.Point(7, 38);
			this.mapListBox.Name = "mapListBox";
			this.mapListBox.Size = new System.Drawing.Size(628, 433);
			this.mapListBox.TabIndex = 3;
			// 
			// btnMap
			// 
			this.btnMap.Location = new System.Drawing.Point(493, 5);
			this.btnMap.Name = "btnMap";
			this.btnMap.Size = new System.Drawing.Size(142, 23);
			this.btnMap.TabIndex = 2;
			this.btnMap.Text = "Показать карту";
			this.btnMap.UseVisualStyleBackColor = true;
			this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
			// 
			// appnameLabel
			// 
			this.appnameLabel.AutoSize = true;
			this.appnameLabel.Location = new System.Drawing.Point(4, 10);
			this.appnameLabel.Name = "appnameLabel";
			this.appnameLabel.Size = new System.Drawing.Size(97, 13);
			this.appnameLabel.TabIndex = 1;
			this.appnameLabel.Text = "Имя приложения:";
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(107, 7);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(380, 20);
			this.tbName.TabIndex = 0;
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(668, 556);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainForm";
			this.Text = "Архитектура памяти";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numberOfPages)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartVirtual)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartPage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartPhys)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartMemoryLoad)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbSystemInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMemoryLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhys;
        private System.Windows.Forms.Label physPercent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label filePrecent;
        private System.Windows.Forms.Label virtualPercent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVirtual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Label appnameLabel;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ListBox mapListBox;
        private System.Windows.Forms.Button btnVirtualAlloc;
        private System.Windows.Forms.Button btnVirtualFree;
        private System.Windows.Forms.NumericUpDown numberOfPages;
        private System.Windows.Forms.Label additionalInfo;
        private System.Windows.Forms.Label numberOfPagesLabel;
    }
}

