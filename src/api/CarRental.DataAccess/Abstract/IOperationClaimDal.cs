using CarRental.Core.DataAccess;
using CarRental.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DataAccess.Abstract
{
    public interface IOperationClaimDal: IEntityRepository<OperationClaim>
    {
        
    }
}
