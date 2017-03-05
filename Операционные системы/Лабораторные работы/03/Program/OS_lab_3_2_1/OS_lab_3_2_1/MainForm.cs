using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OS_lab_3_2_1
{
    public partial class MainForm : Form
    {
        IntPtr semaphore = IntPtr.Zero;
        UInt64 i = 1;

        public MainForm()
        {
            InitializeComponent();
        }

		private void MainForm_Load(object sender, EventArgs e)
        {
            createSemaphore.Enabled = true;
            connectSemaphore.Enabled = true;
            closeSemaphore.Enabled = false;
        }

		private void createSemaphore_Click(object sender, EventArgs e)
		{
			semaphore = win32API.createSemaphore(amountSemaphores.Value, "mySemaphore_lab_31");
			if (semaphore != IntPtr.Zero)
			{
				createSemaphore.Enabled = false;
				connectSemaphore.Enabled = false;
				closeSemaphore.Enabled = true;
				runTimer.Start();
			}
			else
			{
				MessageBox.Show("Ошибка создания семафора");
			}
		}

		private void connectSemaphore_Click(object sender, EventArgs e)
		{
			semaphore = win32API.openSemaphore("mySemaphore_lab_31");
			if (semaphore != IntPtr.Zero)
			{
				createSemaphore.Enabled = false;
				connectSemaphore.Enabled = false;
				closeSemaphore.Enabled = true;
				runTimer.Start();
			}
			else
			{
				MessageBox.Show("Ошибка подключения семафора");
			}
		}

		private void disconnectSemaphore_Click(object sender, EventArgs e)
		{
			if (win32API.closeSemaphore(semaphore))
			{
				semaphore = IntPtr.Zero;
				closeSemaphore.Enabled = false;
				connectSemaphore.Enabled = true;
				createSemaphore.Enabled = true;
				runTimer.Stop();
			}
			else
			{
				MessageBox.Show("Ошибка закрытия семафора");
			}
		}

		private void runTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (semaphore != IntPtr.Zero)
				{
					win32API.pause(semaphore);
					runTimer.Stop();
					stopTimer.Start();
				}
			}
			catch (Exception err)
			{
				runTimer.Stop();
				stopTimer.Stop();
				MessageBox.Show(err.Message);
			}
		}

		private void stopTimer_Tick(object sender, EventArgs e)
		{
			counter.Text = i.ToString();
			i++;
			if (i % 10 == 0)
			{
				if (semaphore != IntPtr.Zero)
				{
					stopTimer.Stop();
					win32API.releaseSemaphore(semaphore);
					runTimer.Start();
				}
			}
		}
    }
}
