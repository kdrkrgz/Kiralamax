using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
