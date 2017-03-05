using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using Course.Utils;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Globalization;
// unsafe!!
using ImapX;

namespace Course.IMAP
{
    class IMAPClient
    {
        private TcpClient _client;
        private StreamWriter _writer;
        private StreamReader _reader;
        private Logger _logger = Logger.Instance;
        private UInt64 _prefix = 0;
        private string _selectedFolder;

        public IMAPClient(string hostname, int port, bool ssl)
        {
            try
            {
                _client = new TcpClient(hostname, port);

                if (ssl)
                {
                    var stream = new SslStream(_client.GetStream());
                    stream.AuthenticateAsClient(hostname);

                    _writer = new StreamWriter(stream);
                    _reader = new StreamReader(stream);
                }
                else
                {
                    var stream = _client.GetStream();

                    _writer = new StreamWriter(stream);
                    _reader = new StreamReader(stream);
                }

                string greeting = _reader.ReadLine();
                _logger.Add(greeting);
            }
            catch (Exception e)
            {
                _logger.AddError(e.Message);
            }
        }

        public IMAPClient(string hostname, string sport, bool ssl)
            : this(hostname, Convert.ToInt32(sport), ssl)
        {
        }

        public void Authenticate(string username, string password)
        {
            this.SendCommand(Commands.Login, username, password);
            _logger.Add(this.GetResponse());
        }

        public List<string> GetFolders()
        {
            this.SendCommand(Commands.List, "", "*");
            string response = this.GetResponse();
            _logger.Add(response);

            string[] lines = response.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> folders = new List<string>();

            foreach (string line in lines)
            {
                MatchCollection m = Regex.Matches(line, "\\\"(.*?)\\\"|(INBOX)");

                if (m.Count > 1)
                {
                    string folderName = m[m.Count - 1].ToString().Trim(new char[] { '"' });
                    if (folderName == "INBOX")
                    {
                        folderName = (string)Properties.Resources.ResourceManager.GetObject("Inbox", CultureInfo.CurrentCulture);
                    }
                    
                    folders.Add(folderName);
                }
            }

            _logger.Add(folders);
            return folders;
        }

        public void SelectFolder(string folderName)
        {
            if (folderName == (string)Properties.Resources.ResourceManager.GetObject("Inbox", CultureInfo.CurrentCulture))
            {
                folderName = "INBOX";
            }

            _selectedFolder = folderName;
            this.SendCommand(Commands.Select, folderName);

            _logger.Add(this.GetResponse());
        }

        public int GetMessageCount()
        {
            this.SendCommand(Commands.Status, this._selectedFolder, "messages");
            string response = this.GetResponse();
            _logger.Add(response);
            Match m = Regex.Match(response, "[0-9]*[0-9]");
            return Convert.ToInt32(m.ToString());
        }

        public int GetUnseenMessageCount()
        {
            this.SendCommand(Commands.Status, this._selectedFolder, "unseen");
            string response = this.GetResponse();
            _logger.Add(response);
            Match m = Regex.Match(response, "[0-9]*[0-9]");
            return Convert.ToInt32(m.ToString());
        }

        public Dictionary<int, string> GetListMessages()
        {
            this.SendCommand(Commands.Fetch, "1:*", "FLAGS UID");
            string response = this.GetResponse();
            _logger.Add(response);

            string[] lines = response.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var messages = new Dictionary<int, string>();

            foreach (string line in lines)
            {
                MatchCollection m = Regex.Matches(line, "[0-9]*[0-9]");

                if (m.Count > 1)
                {
                    int id = Convert.ToInt32(m[0].ToString().Trim(new char[] { '"' }));
                    string uid = m[1].ToString().Trim(new char[] { '"' });

                    messages.Add(id, uid);
                }
            }

            _logger.Add(messages);
            return messages;
        }

        public void Delete(int id)
        {
            this.SendCommand(Commands.Delete, id.ToString());
            _logger.Add(this.GetResponse());
        }

        public void SendCommand(string command, params string[] list)
        {
            var cmd = UTF7Converter.Encode(String.Format(command, list));
            _writer.WriteLine("A" + (_prefix++).ToString() + ' ' + cmd);
            _writer.Flush();
        }  

        private string GetResponse()
        {
            string response = string.Empty;

            while (true)
            {
                string line = UTF7Converter.Decode(_reader.ReadLine());
                string[] tags = line.Split(new char[] { ' ' });
                response += line + Environment.NewLine;
                if (tags[0].Substring(0, 1) == "A"
                    && tags[1].Trim() == "OK"
                    || tags[1].Trim() == "BAD"
                    || tags[1].Trim() == "NO")
                {
                    break;
                }

            }

            return response;
        }

        public string GetMessage(string uid)
        {
            this.SendCommand(Commands.Fetch, uid, "body[text]");
            var message = this._GetMessage();

            return message;
        }

        private string _GetMessage()
        {
            string line = _reader.ReadLine();
            MatchCollection m = Regex.Matches(line, "\\{(.*?)\\}");

            if (m.Count > 0)
            {
                int length = Convert.ToInt32(m[0].ToString().Trim(new char[] { '{', '}' }));

                char[] buffer = new char[length];
                int read = (length < 128) ? length : 128;
                int remaining = length;
                int offset = 0;
                while (true)
                {
                    read = _reader.Read(buffer, offset, read);
                    remaining -= read;
                    offset += read;
                    read = (remaining >= 128) ? 128 : remaining;

                    if (remaining == 0)
                    {
                        break;
                    }
                }

                char[] needful = buffer.Where(x => (x.ToString() != "\r" && x.ToString() != "\n")).ToArray();
                byte[] data = Base64Converter.Decode(new String(needful));

                return Encoding.UTF8.GetString(data).Replace("\0", "");
            }

            _logger.Add(this.GetResponse());

            return "";
        }    
    }
}
