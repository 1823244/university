using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {
        //int[] reg = new int[3];
        //int[] sum1 = { 1, 1, 1};
        //int[] sum2 = { 1, 0, 1};
        int[] reg = new int[8];
        int[] sum1 = { 1, 0, 1, 0, 0, 1, 1, 1};
        int[] sum2 = { 1, 1, 1, 1, 1, 0, 0, 1};

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String informStream = this.textBox1.Text;
            String codMsg = "";


            for(int i = 0; i < informStream.Length; i++) {
                addBit(informStream[i]);
                codMsg += sums();
            }


            this.textBox2.Text = codMsg;
        }

        //Меняем состояние регистра
        private void addBit(char bit)
        {
            for (int i = reg.Length - 2; i >= 0; i--)
            {
                reg[i+1] = reg[i];
            }
            reg[0] = bit == '1' ? 1 : 0;
        }
        //Выходная строка после использования сумматоров
        private string sums()
        {
            int a = 0;
            int b = 0;

            for (int i = 0; i < reg.Length; i++)
            {
                if (sum1[i] == 1)
                {
                    a ^= reg[i];
                }
                if (sum2[i] == 1)
                {
                    b ^= reg[i];
                }
            }

            return " " + a + b;
        }
    }
}
