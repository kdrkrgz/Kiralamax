using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Business.ValidationRules.FluentValidation;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.Aspects.Autofac.Validation;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.CrossCuttingConcerns.Validation;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Business;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using FluentValidation;

namespace CarRental.Business.Concrete
{
    public class CustomerManager: ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        public IDataResult<List<CustomerDetailDto>> GetCustomersWithDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>( _customerDal.GetAllCustomersWithDetails());
        }

        [SecuredOperation("admin,system.admin")]
        public IDataResult<CustomerDetailDto> GetCustomerWithDetails(int id)
        {
            return new SuccessDataResult<CustomerDetailDto>( _customerDal.GetCustomerWithDetails(id));
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<CustomerBaseDto>> GetCustomerBaseList()
        {
            return new SuccessDataResult<List<CustomerBaseDto>>(_customerDal.GetCustomersBaseList());
        }

        public IDataResult<CustomerBaseDto> GetCustomerWithEmail(string email)
        {
            return new SuccessDataResult<CustomerBaseDto>(_customerDal.GetCustomerByEmail(email));
        }

        public IDataResult<Customer> GetCustomerByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.AppUserId == userId));
        }

        public IDataResult<Customer> GetCustomer(int id)
        {
            return new SuccessDataResult<Customer>( _customerDal.Get(x => x.Id == id));
        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        public IDataResult<List<Customer>> GetAllCustomers()
        {
            return new SuccessDataResult<List<Customer>>( _customerDal.GetAll());
        }

        [LogAspect(typeof(FileLogger))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult AddCustomer(Customer customer)
        {

            // ValidationTool.Validate(new CustomerValidator(), customer); Aspect ile hallettik.
            // burada bir çok iş kuralı oalcağından içe içe ifler ve kurallar çoğalacak dolayısıyla karışık bir hal alacak bu yüzden bir business motoru yazmak daha efektif

            //IResult result = BusinessRules.Run(CheckUserEmailExist(customer.AppUser.Email)); // iş kuralına uymayan resultlar motordan dönecek

            //if (result != null) // iş kurallarının hepsi başarılı olduğunda null döneceğinden iş kuralından geçermediğinde hatalı resultı dön
            //{
            //    return result;
            //}

            _customerDal.Add(customer);
            return new SuccessResult(Message.CustomerAdded);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult UpdateCustomer(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Message.CustomerUpdated);

        }
        
        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult DeleteCustomer(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Message.CustomerDeleted);

        }

        // Business codes
        private IResult CheckUserEmailExist(string email)
        {
            var result = _customerDal.GetAll(x => x.AppUser.Email == email);
            if (result.Any())
            {
                return new ErrorResult(Message.UserEmailExist);    
            }
            return new SuccessResult();
        }
    }

}
