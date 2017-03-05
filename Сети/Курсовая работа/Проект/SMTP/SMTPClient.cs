using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Course.Utils;

namespace Course.SMTP
{
    class SMTPClient
    {
        private SmtpClient _client;
        private Logger _logger = Logger.Instance;

        public SMTPClient(string hostname, int port, bool ssl)
        {
            _client = new SmtpClient(hostname, port);
            _client.EnableSsl = ssl;
            _client.Timeout = 10000;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
        }

        public SMTPClient(string hostname, string sport, bool ssl)
            : this(hostname, Convert.ToInt32(sport), ssl)
        {
        }

        public void Authenticate(string username, string password)
        {
            _client.Credentials = new NetworkCredential(username, password);
        }

        public void Send(string from, List<string> to, string subject, string body, List<string> files)
        {
            var message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.HeadersEncoding = Encoding.UTF8;
            message.From = new MailAddress(from);

            foreach (var item in to)
            {
                message.To.Add(item.Trim());
            }

            message.Subject = subject;
            message.Body = body;

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        message.Attachments.Add(new Attachment(file, MediaTypeNames.Application.Octet));
                    }
                }
            }

            try
            {
                _client.Send(message);
            }
            catch (Exception e)
            {
                _logger.Add(e.Message);
            }
        }

        public void Close()
        {
            _client.Dispose();
        }
    }
}
