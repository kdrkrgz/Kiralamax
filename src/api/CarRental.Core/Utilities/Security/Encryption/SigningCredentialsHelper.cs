using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // Encrypt için kullanılacak anahtar ve algortima belirliyoruz.
        // 
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

    }
}
