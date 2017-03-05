using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Course.Mail
{
    public class Address
    {
        public Address()
        {
            DisplayName = string.Empty;
            Email = string.Empty;
        }

        public Address(string display, string addr)
        {
            Email = (addr ?? "").Trim(' ', '\n', '\t');
            DisplayName = (display ?? "").Trim(' ', '\n', '\t');
        }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(DisplayName) ? Email : string.Format("{0} <{1}>", DisplayName, Email);
        }
    }
}
