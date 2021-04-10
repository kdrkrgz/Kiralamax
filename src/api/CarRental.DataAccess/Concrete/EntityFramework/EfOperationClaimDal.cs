using CarRental.Core.DataAccess.EntityFramework;
using CarRental.Core.Entities.Concrete;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal: EfEntityRepositoryBase<CarRentalDbContext, OperationClaim>, IOperationClaimDal
    {
        public EfOperationClaimDal(CarRentalDbContext context) : base(context)
        {

        }
    }
}
