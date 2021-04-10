using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;

namespace CarRental.Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetClaims();
    }
}
