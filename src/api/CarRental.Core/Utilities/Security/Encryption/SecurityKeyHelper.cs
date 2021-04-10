using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        // system identity securitykey içinden gelen security key
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
