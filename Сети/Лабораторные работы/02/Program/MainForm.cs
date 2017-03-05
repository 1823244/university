using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab3
{
    public partial class MainForm : Form
    {
        string mac;

        public MainForm()
        {
            InitializeComponent();

            mac = Utils.GetMAC();
            textLocalMAC.Text = mac;
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
            textMAC.ReadOnly = true;

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
            textMAC.ReadOnly = radioServer.Checked;
        }

        private void ChangeMACField()
        {
            if (radioIPX.Checked)
            {
                if (radioClient.Checked)
                {
                    textMAC.ReadOnly = true;
                    textMAC.Text = "FFFFFFFFFFFF";

                    groupMAC.Text = "Адрес сервера";
                }
                else
                {
                    textMAC.ReadOnly = false;
                    textMAC.Text = "";

                    groupMAC.Text = "Адрес клиента";
                }
            }
            else
            {
                if (radioClient.Checked)
                {
                    textMAC.ReadOnly = false;
                    textMAC.Text = "";

                    groupMAC.Text = "Адрес сервера";
                }
                else
                {
                    textMAC.ReadOnly = false;
                    textMAC.Text = "";

                    groupMAC.Text = "Адрес клиента";
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
            ChangeMACField();
        }

        private void radioIPX_CheckedChanged(object sender, EventArgs e)
        {
            ChangeMACField();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textMAC.Text.Length != 12
                || textSocket.Text.Length < 4)
            {
                return;
            }

            DisableForm();

            var mac = Convert.ToInt64(textMAC.Text, 16);
            var socket = Convert.ToUInt16(textSocket.Text);

            Utils.Starter(radioClient.Checked, radioIPX.Checked, mac, socket, this);

            MessageBox.Show("Готово!");

            EnableForm();
        }

        private void textMAC_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0) return;

            try
            {
                var r = Convert.ToInt64(tb.Text, 16);
                if (r < 0) throw new Exception();
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
