using CarRental.Business.Abstract;
using CarRental.Business.Constants;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace CarRental.Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IResult AddCreditCard(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Message.CreditCardAdded);
        }

        public IResult DeleteCreditCard(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Message.CreditCardDeleted);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IResult DeleteCreditCardById(int creditCardId)
        {
            var creditCardResult = GetCreditCard(creditCardId);
            var result = DeleteCreditCard(creditCardResult.Data);
            if (result.IsSuccess)
            {
                return new SuccessResult(Message.CreditCardDeleted);
            }
            return new SuccessResult(Message.CreditCardDeleteFailed);

        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<CreditCard> GetCreditCard(int creditCardId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(x => x.Id == creditCardId));
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<CreditCard>> GetCustomerCreditCards(int customerId)
        {
            var result = _creditCardDal.GetAll(x => x.CustomerId == customerId);
            return new SuccessDataResult<List<CreditCard>>(result);
        }
    }
}
