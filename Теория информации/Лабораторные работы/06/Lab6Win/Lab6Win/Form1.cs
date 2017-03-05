using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab6Win
{
    public partial class Form1 : Form
    {
        public static int[,] H = {
			{ 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 },
			{ 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1 },
			{ 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }
		};
        public static int[,] G = {
			{ 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
			{ 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0 },
			{ 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
			{ 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0 },
			{ 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0 },
			{ 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }
		};
        public static List<int> contr = new List<int>() { 0, 1, 3, 7 };

        public static int[] Multiplication(int[] a, int[,] b)
        {
            if (a.Length != b.GetLength(0))
            {
                return null;
            }
            int[] c = new int[b.GetLength(1)];

            for (int i = 0; i < b.GetLength(1); i++)
            {
                for (int j = 0; j < b.GetLength(0); j++)
                {
                    c[i] += a[j] * b[j, i];
                }
                c[i] = c[i] % 2;
            }

            return c;
        }

        public static int[] Multiplication(int[,] a, int[] b)
        {
            if (a.GetLength(1) != b.Length)
            {
                return null;
            }
            int[] c = new int[a.GetLength(0)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i] += b[j] * a[i, j];
                }
                c[i] = c[i] % 2;
            }

            return c;
        }

        public static int Syndrome(int[] c)
        {
            int length = c.Length - 1;
            int result = 0;

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 1)
                {
                    result += (int)Math.Pow(2, length - i);
                }
            }

            return result;
        }

        public static string Print(int[] a)
        {
            string res = "";
            for (int i = 0; i < a.Length; i++)
            {
                res += a[i];
            }
            return res;
        }

        //*****************************************************************************************
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 15;
            inf1T.Text =
                code1T.Text =
                codeErrT.Text =
                syndrT.Text =
                codeIsprT.Text =
                inf2T.Text = "";
            confirmErrorBtn.Enabled = false;
            isprBtn.Enabled = false;
            decodingBtn.Enabled = false;
        }

        private void inf1T_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (((TextBox)sender).Text.Length > 10 || e.KeyChar < '0' || e.KeyChar > '1'))
            {
                e.Handled = true;
            }
        }

        private void codingBtn_Click(object sender, EventArgs e)
        {
            if(this.inf1T.Text.Length < 11) {
                MessageBox.Show("Введите информационное слово длиной 11 бит");
                return;
            }
            int[] infWord = new int[11];
            for(int i = 0; i < this.inf1T.Text.Length; i++) {
                infWord[i] = this.inf1T.Text[i] == '1' ? 1 : 0;
            }

            int[] codeWord = Multiplication(infWord, G);
            code1T.Text = Print(codeWord);

            this.confirmErrorBtn.Enabled = true;
        }

        private void cleanAllBtn_Click(object sender, EventArgs e)
        {
            inf1T.Text =
                code1T.Text =
                codeErrT.Text =
                syndrT.Text =
                codeIsprT.Text =
                inf2T.Text = "";
            confirmErrorBtn.Enabled = false;
            isprBtn.Enabled = false;
            decodingBtn.Enabled = false;
        }

        private void confirmErrorBtn_Click(object sender, EventArgs e)
        {
            int[] codeWord = new int[15];
            for (int i = 0; i < this.code1T.Text.Length; i++)
            {
                codeWord[i] = this.code1T.Text[i] == '1' ? 1 : 0;
            }
            codeWord[(int)this.numericUpDown1.Value - 1] = codeWord[(int)this.numericUpDown1.Value - 1] == 1 ? 0 : 1;
            codeErrT.Text = Print(codeWord);

            this.isprBtn.Enabled = true;
        }

        private void isprBtn_Click(object sender, EventArgs e)
        {
            int[] codeWord = new int[15];
            for (int i = 0; i < this.codeErrT.Text.Length; i++)
            {
                codeWord[i] = this.codeErrT.Text[i] == '1' ? 1 : 0;
            }
            int[] syndr = Multiplication(H, codeWord);
            this.syndrT.Text = "" + Syndrome(syndr) + " = " + Print(syndr);

            if (Syndrome(syndr) == 0)
            {
                MessageBox.Show("Ошибок нет");
            }
            else
            {
                codeWord[Syndrome(syndr) - 1] = codeWord[Syndrome(syndr) - 1] == 0 ? 1 : 0;
                this.codeIsprT.Text = Print(codeWord);
                this.decodingBtn.Enabled = true;
            }
        }

        private void decodingBtn_Click(object sender, EventArgs e)
        {
            int[] codeWord = new int[15];
            for (int i = 0; i < this.codeIsprT.Text.Length; i++)
            {
                codeWord[i] = this.codeIsprT.Text[i] == '1' ? 1 : 0;
            }
            for (int i = 0; i < codeWord.Length; i++)
            {
                if (!contr.Contains(i))
                {
                    this.inf2T.AppendText("" + codeWord[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inf1T_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codingBtn.PerformClick();
            }
        }
    }
}
