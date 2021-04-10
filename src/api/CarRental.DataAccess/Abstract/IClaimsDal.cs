using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess;
using CarRental.Core.Entities.Concrete;

namespace CarRental.DataAccess.Abstract
{
    public interface IClaimsDal:IEntityRepository<OperationClaim>
    {
    }
}
