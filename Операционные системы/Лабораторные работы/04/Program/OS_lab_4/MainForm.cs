using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OS_lab3;
using System.Runtime.InteropServices;

namespace OS_lab_4
{
    public partial class MainForm : Form
    {
        private SystemInfo sysInfo;
        private MemoryInfo memInfo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sysInfo = new SystemInfo();

            lbSystemInfo.Text = "Архитектура процессора: " + sysInfo.ProcessorArchitecture +
                "\n" + "Размер страницы: " + sysInfo.PageSize + " байт" +
                "\n" + "Минимальный адрес приложения: 0x" + Convert.ToString(sysInfo.MinimumApplicationAddress.ToInt32(), 16).ToUpper() +
                "\n" + "Максимальный адрес приложения: 0x" + Convert.ToString(sysInfo.MaximumApplicationAddress.ToInt32(), 16).ToUpper() +
                "\n" + "Гранулярность для начального адреса, в котором может быть выделена виртуальная память: " + sysInfo.AllocationGranularity + " байт" +
                "\n" + "Количество процессоров: " + sysInfo.NumberOfProcessors +
                "\n" + "Уровень процессора: " + sysInfo.ProcessorLevel +
                "\n" + "Редакция процессора: " + sysInfo.ProcessorRevision;

            memInfo = new MemoryInfo();
            memInfo.pageSize = sysInfo.PageSize;
            memInfo.pageLimit = 1000;

            updatePies();

            timer1.Start();
        }

        private void updatePies()
        {
            memInfo.update();

            chartMemoryLoad.Series[0].Points.Clear();
            chartMemoryLoad.Series[0].Points.AddXY("Используется, %", memInfo.MemoryLoad);
            chartMemoryLoad.Series[0].Points.AddXY("Свободно, %", 100 - memInfo.MemoryLoad);

            physPercent.Text = "Всего: " + (memInfo.TotalPhys.ToUInt32() / 1024 / 1024) + " Мб";
            chartPhys.Series[0].Points.Clear();
            chartPhys.Series[0].Points.AddXY("Используется, Мб", (memInfo.TotalPhys.ToUInt32() - memInfo.AvailPhys.ToUInt32()) / 1024 / 1024);
            chartPhys.Series[0].Points.AddXY("Свободно, Мб", memInfo.AvailPhys.ToUInt32() / 1024 / 1024);

            filePrecent.Text = "Всего: " + (memInfo.TotalPageFile.ToUInt32() / 1024 / 1024) + " Мб";
            chartPage.Series[0].Points.Clear();
            chartPage.Series[0].Points.AddXY("Используется, Мб", (memInfo.TotalPageFile.ToUInt32() - memInfo.AvailPageFile.ToUInt32()) / 1024 / 1024);
            chartPage.Series[0].Points.AddXY("Свободно, Мб", memInfo.AvailPageFile.ToUInt32() / 1024 / 1024);

            virtualPercent.Text = "Всего: " + (memInfo.TotalVirtual.ToUInt32() / 1024 / 1024) + " Мб";
            chartVirtual.Series[0].Points.Clear();
            chartVirtual.Series[0].Points.AddXY("Используется, Мб", (memInfo.TotalVirtual.ToUInt64() - memInfo.AvailVirtual.ToUInt64()) / (1024 * 1024));
            chartVirtual.Series[0].Points.AddXY("Свободно, Мб", memInfo.AvailVirtual.ToUInt64() / (1024 * 1024));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatePies();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr handle = win32API22.getProcessHandle(tbName.Text);
                List<String> list = VirtualMap.GetVirtualMap(handle);

                mapListBox.Items.Clear();
                foreach (String tmp in list)
                {
                    mapListBox.Items.Add(tmp);
                }
                additionalInfo.Text = "Всего страниц: " + VirtualMap.countPage;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnVirtualAlloc_Click(object sender, EventArgs e)
        {
            try
            {
                memInfo.VirtualAlloc();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                memInfo.VirtualFree();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnVirtualAlloc_Click_1(object sender, EventArgs e)
        {
            try
            {
                memInfo.pageLimit = (uint)numberOfPages.Value;
                memInfo.VirtualAlloc();

                int error = win32LastError.GetIntError();
                if (error != 0)
                {
                    MessageBox.Show(win32LastError.GetStringError(error));
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                memInfo.VirtualFree();

                int error = win32LastError.GetIntError();
                if (error != 0)
                {
                    MessageBox.Show(win32LastError.GetStringError(error));
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
