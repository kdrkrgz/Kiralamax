using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using CarRental.Core.Entities.Concrete;

namespace CarRental.Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
