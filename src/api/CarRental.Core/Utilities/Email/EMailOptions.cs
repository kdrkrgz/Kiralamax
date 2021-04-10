using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Core.Utilities.Email
{
    public class EMailOptions
    {
        public string FromMailAddress { get; set; }
        public string Password { get; set; }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }

        public string ToMailAddress { get; set; }

    }
}
