using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Course.SMTP;

namespace Course.Tools
{
    public partial class NewMail : Form
    {
        SMTPClient smtp;
        Properties.Settings settings = Properties.Settings.Default;

        public NewMail()
        {
            InitializeComponent();

            smtp = new SMTPClient(settings.SMTPServer, settings.SMTPPort, settings.SMTPSSL);
            smtp.Authenticate(settings.Username, settings.Password);
        }

        public NewMail(List<string> to, string subject)
        {
            InitializeComponent();

            toTextBox.Text = String.Join(",", to.ToArray());
            subjectTextBox.Text = subject;

            smtp = new SMTPClient(settings.SMTPServer, settings.SMTPPort, settings.SMTPSSL);
            smtp.Authenticate(settings.Username, settings.Password);
        }

        private void SendMail()
        {
            var to = toTextBox.Text.Split(',').ToList<string>();
            var files = filesTextBox.Text.Split(',').ToList<string>();

            smtp.Send(settings.Email, to, subjectTextBox.Text, messageTextBox.Text, files);
            this.Close();
        }

        private void FindFiles()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileOk += new CancelEventHandler(dialog_FileOk);
            dialog.Multiselect = true;
            dialog.ShowDialog();
        }

        #region manual events

        private void dialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                filesTextBox.Text = String.Join(",", ((OpenFileDialog)sender).FileNames);
            }
        }

        #endregion

        #region events

        private void NewMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            smtp.Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMail();
        }

        private void attachButton_Click(object sender, EventArgs e)
        {
            FindFiles();
        }

        #endregion
    }
}
