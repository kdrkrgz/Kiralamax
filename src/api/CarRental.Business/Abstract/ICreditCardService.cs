using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Business.Abstract
{
    public interface ICreditCardService
    {
        IResult AddCreditCard(CreditCard creditCard);
        IResult DeleteCreditCard(CreditCard creditCard);
        IResult DeleteCreditCardById(int creditCardId);
        IDataResult<List<CreditCard>> GetCustomerCreditCards(int customerId);
        IDataResult<CreditCard> GetCreditCard(int creditCardId);
    }
}
