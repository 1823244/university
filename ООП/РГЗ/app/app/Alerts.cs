using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public partial class Alerts : Form
    {
        private Dictionary<string, int> late = new Dictionary<string, int>();
        private Dictionary<string, int> early = new Dictionary<string, int>();
        private string[,] alerts = new string[7, 2];
        public Alerts()
        {
            InitializeComponent();
        }
         
        public Alerts(Dictionary<string, int> first, Dictionary<string, int> second)
        {
            InitializeComponent();
            late = first;
            early = second;
        }

        private void Alerts_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (KeyValuePair<string, int> pair in late)
            {
                alerts[i, 0] = pair.Key;
                alerts[i, 1] = "Опоздал на " + Convert.ToString(pair.Value) + " с";
                i++;
            }

            foreach (KeyValuePair<string, int> pair in early)
            {
                alerts[i, 0] = pair.Key;
                alerts[i, 1] = "Задержался на " + Convert.ToString(pair.Value) + " с";
                i++;
            }

            i--;
            for (; i >= 0; i--)
            {
                alertsTable[i + 1, 1] = alerts[i, 0];
                alertsTable[i + 1, 2] = alerts[i, 1];
            }
        }
    }
}
