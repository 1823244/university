using System;
using System.Text;
using System.Windows.Forms;
using Course.Utils;

namespace Course.Tools
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        #region events


        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        #endregion

        private void yandexButton_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "Igor Adamenko";
            emailTextBox.Text = "sir.pupo4ek@yandex.ru";
            usernameTextBox.Text = "sir.pupo4ek";
            passwordTextBox.Text = Encoding.Default.GetString(Base64Converter.Decode("d29sZjE1ODQ="));
            IMAPAddressTextBox.Text = "imap.yandex.ru";
            IMAPPortTextBox.Text = "993";
            SMTPAddressTextBox.Text = "smtp.yandex.ru";
            SMTPPortTextBox.Text = "465";
        }

        private void gmailButton_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "Igor Adamenko";
            emailTextBox.Text = "gnu.dante@gmail.com";
            usernameTextBox.Text = "gnu.dante";
            passwordTextBox.Text = Encoding.Default.GetString(Base64Converter.Decode("cXlzdG9tdHZ4aGZ4amNyZQ=="));
            IMAPAddressTextBox.Text = "imap.gmail.com";
            IMAPPortTextBox.Text = "993";
            SMTPAddressTextBox.Text = "smtp.gmail.com";
            SMTPPortTextBox.Text = "465";
        }
    }
}
