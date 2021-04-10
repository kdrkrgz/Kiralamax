using CarRental.Core.DataAccess;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DataAccess.Abstract
{
    public interface ICreditCardDal:IEntityRepository<CreditCard>
    {
    }
}
