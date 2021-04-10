using CarRental.Core.DataAccess.EntityFramework;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfCreditCardDal: EfEntityRepositoryBase<CarRentalDbContext, CreditCard>, ICreditCardDal
    {

        public EfCreditCardDal(CarRentalDbContext context):base(context)
        {

        }
    }
}
