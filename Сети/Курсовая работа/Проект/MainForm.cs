using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Course.IMAP;
using Course.Utils;
using Course.Mail;
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace Course
{
    public partial class MainForm : Form
    {
        Logger _logger = Logger.Instance;
        IMAPClient imap;
        List<Email> messages = new List<Email>();
        ImapX.Message[] _messages;
        ImapX.ImapClient _client;
        Properties.Settings settings = Properties.Settings.Default;
        string folderPath = @"C:\Course\Temp";

        public MainForm()
        {
            InitializeComponent();

            imap = new IMAPClient(settings.IMAPServer, settings.IMAPPort, settings.IMAPSSL);
            imap.Authenticate(settings.Username, settings.Password);

            UpdateFolders();
        }

        private void UpdateFolders()
        {
            var folders = imap.GetFolders();
            foldersTreeView.BeginUpdate();
            foldersTreeView.Nodes.Clear();

            var inbox = (string)Properties.Resources.ResourceManager.GetObject("Inbox", CultureInfo.CurrentCulture);

            if (folders.Contains(inbox))
            {
                foldersTreeView.Nodes.Add(inbox);
                folders.Remove(inbox);
            }

            foreach (var folder in folders)
            {
                foldersTreeView.Nodes.Add(folder);
            }

            foldersTreeView.EndUpdate();
        }

        private void ReloadMessages()
        {
            imap.SelectFolder(foldersTreeView.SelectedNode.Text);

            totalNumberToolStripStatusLabel.Text = imap.GetMessageCount().ToString();
            unseenNumberToolStripStatusLabel.Text = imap.GetUnseenMessageCount().ToString();

            var list = imap.GetListMessages();
            var messages = new List<string>();

            foreach (var item in list)
            {
                //messages.Add(imap.GetMessage(item.Value));
            }

            _logger.Add(messages);

            maillistDataGridView.Rows.Clear();

            // fucking unsafe!!
            _client = new ImapX.ImapClient(settings.IMAPServer, Convert.ToInt32(settings.IMAPPort), settings.IMAPSSL, validateServerCertificate: true);
            _client.Behavior.AutoPopulateFolderMessages = true;

            if (_client.Connect())
            {

                if (_client.Login(settings.Username, settings.Password))
                {
                    var folderName = foldersTreeView.SelectedNode.Text;
                    if (folderName == (string)Properties.Resources.ResourceManager.GetObject("Inbox", CultureInfo.CurrentCulture))
                    {
                        folderName = "INBOX";
                    }

                    _messages = _client.Folders[folderName].Search();

                    maillistDataGridView.Rows.Clear();

                    foreach (var item in _messages)
                    {
                        maillistDataGridView.Rows.Add(item.UId.ToString(), item.Subject, item.From.Address);

                        System.IO.Directory.CreateDirectory(folderPath);

                        foreach (var file in item.Attachments)
                        {
                            file.Download();
                            file.Save(folderPath);
                        }
                    }
                }
            }
            
        }

        #region events

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new Course.Tools.Settings();
            settings.ShowDialog();
        }

        private void showLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logs = new Course.Tools.Logs();
            logs.Show();
        }

        private void foldersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ReloadMessages();
        }

        private void newMailToolStripButton_Click(object sender, EventArgs e)
        {
            var newmail = new Course.Tools.NewMail();
            newmail.Show();
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            var settings = new Course.Tools.Settings();
            settings.ShowDialog();
        }

        private void getMailToolStripButton_Click(object sender, EventArgs e)
        {
            ReloadMessages();
        }

        private void maillistDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (maillistDataGridView.SelectedRows.Count > 0)
            {
                var mail = _messages[maillistDataGridView.SelectedRows[0].Index];

                mailRichTextBox.Text = mail.Body
                                        .Text.Split(new string[] { "IMAPX" }, StringSplitOptions.None)[0].TrimEnd(' ', ')', '\r', '\n');
                attachmentsTextBox.Text = "";

                foreach (var item in mail.Attachments)
                {
                    attachmentsTextBox.Text += item.FileName + " (" + (item.FileSize / 1024).ToString() + " Kb), ";
                }

                if (attachmentsTextBox.Text.Length > 1)
                {
                    attachmentsTextBox.Text = attachmentsTextBox.Text.Substring(0, attachmentsTextBox.Text.Length - 2);
                }
            }
        }

        #endregion

        private void mailDeleteToolStripButton_Click(object sender, EventArgs e)
        {
            imap.Delete(Convert.ToInt32(maillistDataGridView.SelectedRows[0].Cells["UID"].Value.ToString()));

            _messages[maillistDataGridView.SelectedRows[0].Index].Remove();
            ReloadMessages();
        }

        private void saveToButton_Click(object sender, EventArgs e)
        {
            Process.Start(folderPath);
        }

        private void mailSaveToolStripButton_Click(object sender, EventArgs e)
        {
            var mail = _messages[maillistDataGridView.SelectedRows[0].Index];
            var text = (string)Properties.Resources.ResourceManager.GetObject("Subject", CultureInfo.CurrentCulture) +
                        mail.Subject + Environment.NewLine + (string)Properties.Resources.ResourceManager.GetObject("From", CultureInfo.CurrentCulture) +
                        mail.From.Address + Environment.NewLine + Environment.NewLine + mailRichTextBox.Text;

            FileStream fcreate = File.Open(folderPath + "\\message.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fcreate);
            sw.Write(text);
            sw.Flush();
            fcreate.Close();

            string newfolder = folderPath + "\\message-" + mail.From.Address + mail.UId;

            
            if (System.IO.Directory.Exists(newfolder))
            {
                System.IO.Directory.Move(newfolder, newfolder + DateTime.Now.Millisecond);
            }

            System.IO.Directory.CreateDirectory(newfolder);
            System.IO.File.Move(folderPath + "\\message.txt", newfolder + "\\message.txt");

            foreach (var item in mail.Attachments)
            {
                System.IO.File.Copy(folderPath + '\\' + item.FileName, newfolder + '\\' + item.FileName);
            }

            Process.Start(newfolder);
        }
    }
}
