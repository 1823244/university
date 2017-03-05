using System;
using System.Linq;
using System.Windows.Forms;
using System.Net;

namespace Lab3
{
    public partial class MainForm : Form
    {
        string ip;

        public MainForm()
        {
            InitializeComponent();

            ip = Utils.GetIPString();
            textLocalIP.Text = ip;
        }

        public void UpdateProgess(int value)
        {
            if (value < 0 || value > 100) return;
            progressBar.Value = value;
        }

        public void UpdateStatus(string str)
        {
            if (str == null || str.Length == 0)
            {
                statusValue.Text = "";
            }
            else
            {
                statusValue.Text = str;
            }
        }

        public void DisableForm()
        {
            var l = this.Controls.OfType<GroupBox>();

            textSocket.ReadOnly = true;
            buttonStart.Enabled = false;
            textIP.ReadOnly = true;

            foreach (var groupbox in l)
            {
                groupbox.Enabled = false;
            }
        }

        public void EnableForm()
        {
            var l = this.Controls.OfType<GroupBox>();

            foreach (var groupbox in l)
            {
                groupbox.Enabled = true;
            }

            textSocket.ReadOnly = false;
            buttonStart.Enabled = true;
            textIP.ReadOnly = radioServer.Checked;
        }

        private void ChangeIPField()
        {
            if (radioDgram.Checked)
            {
                if (radioClient.Checked)
                {
                    textIP.ReadOnly = true;
                    textIP.Text = "Broadcast";

                    groupIP.Text = "Адрес сервера";
                }
                else
                {
                    textIP.ReadOnly = false;
                    textIP.Text = "";

                    groupIP.Text = "Адрес клиента";
                }
            }
            else
            {
                if (radioClient.Checked)
                {
                    textIP.ReadOnly = false;
                    textIP.Text = "";

                    groupIP.Text = "Адрес сервера";
                }
                else
                {
                    textIP.ReadOnly = false;
                    textIP.Text = "";

                    groupIP.Text = "Адрес клиента";
                }
            }
        }

#region events

        private void textSocket_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            
            if (tb.Text.Length == 0) return;

            try
            {
                var r = Convert.ToInt32(tb.Text);
                if (r < 0) throw new Exception();
            }
            catch
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 1);
                tb.SelectionStart = tb.Text.Length;
            }
        }

        private void radioClient_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIPField();
        }

        private void radioIPX_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIPField();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textSocket.Text.Length < 4)
            {
                return;
            }

            DisableForm();

            var socket = Convert.ToUInt16(textSocket.Text);

            Utils.Starter(radioClient.Checked, radioDgram.Checked, textIP.Text, socket, this);

            MessageBox.Show("Готово!");

            EnableForm();
        }

        private void textIP_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0) return;

            try
            {
                var r = IPAddress.Parse(tb.Text);
                if (r == null) throw new Exception();
            }
            catch
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 1);
                tb.SelectionStart = tb.Text.Length;
            }
        }

#endregion

    }
}
