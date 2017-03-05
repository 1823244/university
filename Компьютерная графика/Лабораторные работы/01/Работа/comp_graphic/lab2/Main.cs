using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            labelMinValue.Text = chart.Min.ToString();
            labelMaxValue.Text = chart.Max.ToString();

        }

        private void numericUpDownMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chart.Min = (float)Convert.ToDouble(numericUpDownMin.Text);
                labelMinValue.Text = chart.Min.ToString();
                chart.Invalidate();
            }
        }

        private void numericUpDownMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chart.Max = (float)Convert.ToDouble(numericUpDownMax.Text);
                labelMaxValue.Text = chart.Max.ToString();
                chart.Invalidate();
            }
        }
    }
}
