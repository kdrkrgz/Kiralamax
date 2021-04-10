﻿using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess.EntityFramework;
using CarRental.Core.Entities.Concrete;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfClaimsDal: EfEntityRepositoryBase<CarRentalDbContext, OperationClaim>, IOperationClaimDal
    {
        public EfClaimsDal(CarRentalDbContext context):base(context)
        {
            
        }
    }
}
