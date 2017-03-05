using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Course.Mail
{
    public class Email
    {
        public string MessageId { get; set; }

        public Address From { get; set; }
        public Address Sender { get; set; }
        public List<Address> To { get; set; }
        public List<Address> Cc { get; set; }
        public List<Address> Bcc { get; set; }
        public List<Address> ReplyTo { get; set; }
        public string InReplyTo { get; set; }
        public DateTime? Date { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
