using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult DeleteUserOperationClaimsByUserId(int userId);
        IResult AddUserOperationClaim(AppUser userResult, OperationClaim operationClaim);
    }
}
