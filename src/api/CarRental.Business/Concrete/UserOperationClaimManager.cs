using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Entities;

namespace CarRental.Business.Concrete
{
    public class UserOperationClaimManager: IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserServiceService.Get")]
        public IResult DeleteUserOperationClaimsByUserId(int userId)
        {
            var userOperationClaimList = _userOperationClaimDal.GetAll(x => x.UserId == userId);
            foreach (var userOperationClaim in userOperationClaimList)
            {
                _userOperationClaimDal.Delete(userOperationClaim);
            };
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserServiceService.Get")]
        public IResult AddUserOperationClaim(AppUser userResult, OperationClaim operationClaim)
        {

            _userOperationClaimDal.Add(new UserOperationClaim{User = userResult, OperationClaim = operationClaim});
            return new SuccessResult();
        }
    }
}
