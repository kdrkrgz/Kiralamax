using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace CarRental.Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
