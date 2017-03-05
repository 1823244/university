using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SharpLZW;

namespace LZW_WF
{
    public partial class Form1 : Form
    {
        static string textToCompress = null;
        static string tmpFile = "tmp.txt";
        static double before = 0;
        static double after = 0;
        public Form1()
        {
            InitializeComponent();
            ANSI ascii = new ANSI();
            ascii.WriteToFile();
            File.WriteAllText(tmpFile, "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textToCompress = File.ReadAllText(file, System.Text.Encoding.Default);
                button3.Text = "Файл выбран";
                textBox1.Text = textToCompress;
                FileInfo f = new FileInfo(file);
                label4.Text = f.Length + " байт";
                before = f.Length;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textToCompress == null)
            {
                MessageBox.Show("Выберите файл для кодирования!");
                return;
            }
            LZWEncoder encoder = new LZWEncoder();
            byte[] b = encoder.EncodeToByteList(textToCompress);
            File.WriteAllBytes(tmpFile, b);
            textBox2.Text = File.ReadAllText(tmpFile, System.Text.UTF8Encoding.UTF8);
            FileInfo f = new FileInfo(tmpFile);
            label5.Text = f.Length + " байт";
            after = f.Length;
            double procent = (before - after) / before * 100.00;
            labelCompress.ForeColor = Color.Green;
            labelCompress.Text = "Сжато на " + String.Format("{0:0.00}", procent) + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (labelCompress.Text == "")
            {
                MessageBox.Show("Сперва нужно закодировать!");
                return;
            }
            LZWDecoder decoder = new LZWDecoder();
            byte[] b = File.ReadAllBytes(tmpFile);
            string decodedOutput = decoder.DecodeFromCodes(b);
            textBox3.Text = decodedOutput;
            label6.Text = label4.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
