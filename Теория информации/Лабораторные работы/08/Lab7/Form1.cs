using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        Poly infSlovo = new Poly();
        Poly codSlovo = new Poly();
        Poly codSlovoErr = new Poly();
        Poly syndrome = new Poly();
        Poly isprSlovo = new Poly();
        Poly poragdaushiy = new Poly();
        int error;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCod_Click(object sender, EventArgs e)
        {
            poragdaushiy.setPoly("111010001");
            infSlovo.setPoly(this.tb1.Text);
            Poly x = new Poly(); x.setStepen(8);
            codSlovo = infSlovo.ymnoj(x).plus(infSlovo.ymnoj(x).ostdelenie(poragdaushiy));
            tb2.Text = codSlovo.text().PadLeft(15, '0');
        }

        private void btnErr_Click(object sender, EventArgs e)
        {
            int index = (int)numerErr.Value;
            codSlovo.coef.CopyTo(codSlovoErr.coef, 0);
            codSlovoErr.coef[index] = codSlovoErr.coef[index] == 1 ? 0 : 1;
            tb3.Text = codSlovoErr.text().PadLeft(15, '0');
        }

        private void btnDecod_Click(object sender, EventArgs e)
        {
            syndrome = codSlovoErr.ostdelenie(poragdaushiy);
            if (syndrome.text() == "1") { error = 0; }
            else if (syndrome.text() == "10") { error = 1; }
            else if (syndrome.text() == "100") { error = 2; }
            else if (syndrome.text() == "1000") { error = 3; }
            else if (syndrome.text() == "10000") { error = 4; }
            else if (syndrome.text() == "100000") { error = 5; }
            else if (syndrome.text() == "1000000") { error = 6; }
            else if (syndrome.text() == "10000000") { error = 7; }
            else if (syndrome.text() == "11010001") { error = 8; }
            else if (syndrome.text() == "1110011") { error = 9; }
            else if (syndrome.text() == "11100110") { error = 10; }
            else if (syndrome.text() == "11101") { error = 11; }
            else if (syndrome.text() == "111010") { error = 12; }
            else if (syndrome.text() == "1110100") { error = 13; }
            else if (syndrome.text() == "11101000") { error = 14; }
            tb4.Text = syndrome.text() + " = " + error;

            isprSlovo = codSlovoErr.plus(new Poly().setStepen(error));
            tb5.Text = isprSlovo.text().PadLeft(15, '0');

            tb6.Text = tb5.Text.Substring(0, 7).PadLeft(7, '0');

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tb1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCod.PerformClick();
            }
        }

        private void tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar)) return;
            if (tb1.Text.Length > 0 && !(e.KeyChar == '0' || e.KeyChar == '1'))
            {
                e.Handled = true;
            }
        }
    }
}
