using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Resources;
using System.Collections;
using System.IO;

namespace app
{
    public partial class Trains : Form
    {
        Board schedule = new Board();
        public Trains()
        {
            InitializeComponent();
        }

        private void Metro_Load(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            currentTime.Text = time.ToLongTimeString();
            lastTrain.Text = time.ToLongTimeString();

            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    board[i, j] = "";
                }
            }

            LoadAds();
        }

        private void LoadAds()
        {
            int k = 0;
            ResXResourceReader resx = new ResXResourceReader(@"D:\Dropbox\БГТУ\ООП\РГЗ\app\app\Ads.resx");
            string longads = "                                                                                                                         ";
            foreach (DictionaryEntry entry in resx)
            {
                longads += "                                        ";
                longads += entry.Value;
                if (k++ == 9) break;
            }
            ads.Text = longads;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            currentTime.Text = time.ToLongTimeString();
            //if (time.ToLongTimeString().Split(':')[2] == "00")
            //{
            //    schedule.ReadResources();
            //    LoadAds();
            //}

            int train = schedule.Check(time.ToLongTimeString());
            if (train < 0)
            {
                train = -train;
                for (int i = 1; i < 8; i++) {
                    if ((string)board[i, 1] == schedule.Data[train - 1, 0]) 
                    {
                        train = i;
                        lastTrain.Text = schedule.Data[train - 1, 3];
                        break;
                    }
                }
                for (int i = train; i < 7; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        board[i, j] = board[i + 1, j];
                    }
                }

                for (int i = 1; i < 5; i++)
                {
                    board[7, i] = "";
                }
            } else if (train > 0) {
                int i = 1;
                while ((string)board[i, 1] != "") i++;
                
                for (int j = 1; j < 5; j++)
                {
                    board[i, j] = schedule.Data[train - 1, j - 1];
                }
            }
        }

        private void violations_Click(object sender, EventArgs e)
        {
            Alerts alerts = new Alerts(schedule.NoArrive, schedule.NoAway);
            alerts.Show();
        }

        private void fastTimer_Tick(object sender, EventArgs e)
        {
            if (ads.Text[0] == ' ')
            {
                ads.Text = ads.Text.Substring(2, ads.Text.Length - 2) + ads.Text[0] + ads.Text[1];
            }
            else
            {
                ads.Text = ads.Text.Substring(1, ads.Text.Length - 1) + ads.Text[0];
            }      
        }
    }
}
