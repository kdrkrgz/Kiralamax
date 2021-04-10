using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace CarRental.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ICustomerService _customerService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        public UserManager(IUserDal userDal, ICustomerService customerService, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _userDal = userDal;
            _customerService = customerService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        [SecuredOperation("admin,system.admin")]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        [SecuredOperation("admin,system.admin")]
        public IDataResult<List<OperationClaim>> GetClaimsByUserId(int id)
        {
            var user = _userDal.Get(x => x.Id == id);
            if (user == null)
            {
                return new ErrorDataResult<List<OperationClaim>>();
            }
            return new SuccessDataResult<List<OperationClaim>>(GetClaims(user));
        }
        
        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserServiceService.Get")]
        public void Add(AppUser user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(x => x.Email == email);
        }

        [SecuredOperation("admin,system.admin")]
        public IDataResult<UserDataDto> GetUserDetails(string email)
        {
            return new SuccessDataResult<UserDataDto>(_userDal.GetUserDetails(email));
        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        public IDataResult<List<UserDataDto>> GetUsers()
        {
            return new SuccessDataResult<List<UserDataDto>>(_userDal.GetUsers());
        }

        [CacheRemoveAspect("IUserServiceService.Get")]
        [TransactionScopeAspect]
        public IResult UpdateUser(AppUser user, Customer customer)
        {
            var result = GetUserById(user.Id);
            if (!result.IsSuccess)
            {
                return new ErrorResult(Message.UserUpdateFailed);
            }
            result.Data.FirstName = user.FirstName;
            result.Data.LastName = user.LastName;
            result.Data.Email = user.Email;
            result.Data.Status = user.Status;
            _userDal.Update(result.Data);

            var customerResult = _customerService.GetCustomer(user.CustomerId);
            customerResult.Data.CompanyName = customer.CompanyName;
            var customerUpdateResult = _customerService.UpdateCustomer(customerResult.Data);
            if (!customerUpdateResult.IsSuccess)
            {
                return new ErrorResult(Message.UserUpdateFailed);
            }
            return new SuccessResult(Message.UserUpdated);

        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IUserServiceService.Get")]
        public IResult UpdateUserClaims(UserClaimUpdateDto userClaimData)
        {
            // get user
            var userResult = GetByMail(userClaimData.Useremail);
            if (userResult == null)
            {
                return new ErrorResult(Message.UserRolesUpdateError);
            }
            // Get All claims
            var operationClaims = _operationClaimService.GetClaims();
            var userOperationClaims = new List<UserOperationClaim>();
            _userOperationClaimService.DeleteUserOperationClaimsByUserId(userResult.Id);

            foreach (var claimId in userClaimData.Claimids)
            {
                userOperationClaims.Add(new UserOperationClaim { User = userResult, OperationClaimId = claimId });
            }
            userResult.UserOperationClaims = userOperationClaims;
            _userDal.Update(userResult as AppUser);

            return new SuccessResult(Message.UserRolesUpdated);

        }

        public IResult UpdateUserCustomer(AppUser user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("IUserServiceService.Get")]
        public IResult DeleteUser(string userEmail)
        {
            var result = GetByMail(userEmail) as AppUser;
            _userDal.Delete(result);
            return new SuccessResult(Message.UserDeleted);
        }

        private IDataResult<AppUser> GetUserById(int userId)
        {
            var user = _userDal.Get(x => x.Id == userId);
            if (user != null)
            {
                return new SuccessDataResult<AppUser>(user);
            }
            return new ErrorDataResult<AppUser>("Kullanıcı Getirilemedi");
        }

    }
}
