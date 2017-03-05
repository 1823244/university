using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace OS_lab3
{
    public partial class MainForm : Form
    {
        bool flagFoundedProcess = false;

        public MainForm()
        {
            InitializeComponent();
        }

		private void mainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!flagFoundedProcess)
                {
                    IntPtr handleForPause = win32API.getProcessHandle(processName.Text);

                    DateTime start = DateTime.Now;
                    win32API.pauseProcess(handleForPause, UInt32.Parse(timeSleep.Text) * 1000, flagFoundedProcess);
                    
					errors.Text = "Ошибок не обнаружено";
					errors.ForeColor = Color.Green;
                    
					DateTime stop = DateTime.Now;
                    
					int result = ((stop.Hour - start.Hour) * 3600 + (stop.Minute - start.Minute) * 60 + (stop.Second - start.Second)) * 1000 + (stop.Millisecond - start.Millisecond);
					
					if (result > 0)
					{
						programIdle.Text = "" + result;
					}

                    flagFoundedProcess = true;
                }
            }
            catch (Exception exception)
            {
                errors.Text = exception.Message;
				errors.ForeColor = Color.Red;
                Debug.WriteLine(exception.Message);
				mainTimer.Stop();
            }
        }

		private void letsGo_Click(object sender, EventArgs e)
        {
            if (timeSleep.Text.Length > 0 && processName.Text.Length > 0)
            {
                flagFoundedProcess = false;
                mainTimer.Start();
            }
            else
            {
                errors.Text = "Введите время ожидания и имя процесса";
            }
        }
    }
}
