using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctrlFloatNumbersLib
{
    public partial class ctrlFloatNumbers: UserControl
    {
        public ctrlFloatNumbers()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            InitializeComponent();
            textBox.Text = "0";
            StepUp = 1;
            StepDown = 1;
            MinValue = double.MinValue;
            MaxValue = double.MaxValue;
            Checker();
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double StepUp { get; set; }
        public double StepDown { get; set; }

        public double Value
        {
            get
            {
                return Convert.ToDouble(textBox.Text);
            }
            set
            {
                textBox.Text = value.ToString();
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        public int Integer
        {
            get
            {
                return Convert.ToInt32(Math.Truncate(Value));
            }
            set
            {
                Value = value + Fractional;
            }
        }

        public string String
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value; 
            }
        }
        
        public double Fractional
        {
            get
            {
                return Value - Integer;
            }
            set
            {
                Value = value + Integer;
            }
        }

        private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Up) && ((Value + StepUp) <= MaxValue))
            {
                Value += StepUp;
            }

            if ((e.KeyCode == Keys.Down) && ((Value - StepDown) >= MinValue))
            {
                Value -= StepDown;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar == '.' && !textBox.Text.Contains('.') && textBox.Text.Length >= 1) || Char.IsDigit(e.KeyChar)) || e.KeyChar == 8)
            {
                base.OnKeyPress(e);
                Checker();
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Checker()
        {
            if (Value > MaxValue)
            {
                Value = MaxValue;
            }

            if (Value < MinValue)
            {
                Value = MinValue;
            }
        }
    }
}