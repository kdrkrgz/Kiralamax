using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Email;
using CarRental.Core.Utilities.Results;
using CarRental.Core.Utilities.Security.Hashing;
using CarRental.Core.Utilities.Security.JWT;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using log4net.DateFormatter;

namespace CarRental.Business.Concrete
{
    public class AuthManager : IAuthService // 3:44:05 TODO: AuthManager Yazılacak
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private ICustomerService _customerService;
        private IOperationClaimService _operationClaimService;
        private IUserOperationClaimService _userOperationClaimService;
        private IEMailService _emailService;

        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            ICustomerService customerService,
            IOperationClaimService operationClaimService,
            IUserOperationClaimService userOperationClaimService,
            IEMailService emailService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _emailService = emailService;
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new AppUser
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = false,
                ActivationCode = Guid.NewGuid()
            };

            _userService.Add(user);
            var userResult = _userService.GetByMail(user.Email) as AppUser;
            var customerResult = AddUserToCustomer(userResult);

            var operationClaims = _operationClaimService.GetClaims();
            var operationClaim = operationClaims.Data.FirstOrDefault(x => x.Name == "customer");

            _userOperationClaimService.AddUserOperationClaim(userResult, operationClaim);
            userResult.CustomerId = customerResult.Data.Id;
            _userService.UpdateUserCustomer(userResult);
            SendActivationToken(user);
            return new SuccessDataResult<User>(null, Message.UserRegistered);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound);
            }
            if (!userToCheck.Status)
            {
                return new ErrorDataResult<User>(Message.UserNotActivated);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Message.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Message.LoginSuccess);
        }

        [LogAspect(typeof(FileLogger))]
        public IResult UserExist(string userEmail)
        {
            if (_userService.GetByMail(userEmail) != null)
            {
                return new ErrorResult(Message.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Message.AccessTokenCreated);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        private IDataResult<Customer> AddUserToCustomer(AppUser appUser)
        {
            Customer customer = new Customer { CompanyName = " ", AppUser = appUser };
            _customerService.AddCustomer(customer);
            var result = _customerService.GetCustomerByUserId(appUser.Id);
            if (!result.IsSuccess)
            {
                return new ErrorDataResult<Customer>(result.Message);
            }

            return new SuccessDataResult<Customer>(result.Data);

        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IResult ForgotPassword(string userEmail)
        {
            var userResult = _userService.GetByMail(userEmail);
            if (userResult == null)
            {
                return new ErrorResult(Message.UserNotFound);
            }
            _emailService.Send("Kiralamax Parola Sıfırlama", $"Parolanızı aşağıdaki bağlantıya tıklayarak " +
                "sıfırlayabilirsiniz. \n <a target='_black' href='http://localhost:4200/resetpassword/" + $"{userResult.Email}/{userResult.ActivationCode}'>Parolamı Sıfırla</a>", userResult.Email);

            return new SuccessResult(Message.PasswordResetMailSend);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IResult ResetPassword(PasswordResetDto passwordResetDto)
        {
            var userResult = _userService.GetByMail(passwordResetDto.UserEmail) as AppUser;
            if (userResult == null)
            {
                return new ErrorResult(Message.UserNotFound);
            }
            if (userResult.ActivationCode != passwordResetDto.Code)
            {
                return new ErrorResult(Message.UserNotFound);
            }

            userResult.ActivationCode = Guid.NewGuid();

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(passwordResetDto.Password, out passwordHash, out passwordSalt);
            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;
            _userService.UpdateUserCustomer(userResult);
            return new SuccessResult(Message.PasswordReseted);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(FileLogger))]
        public IResult ActivateUser(AccountActivateDto accountActivateDto)
        {
            var userResult = _userService.GetByMail(accountActivateDto.UserEmail) as AppUser;
            if (userResult == null)
            {
                return new ErrorResult(Message.UserActivateFailed);
            }
            if (userResult.ActivationCode != accountActivateDto.Code)
            {
                return new ErrorResult(Message.UserActivateFailed);
            }

            userResult.ActivationCode = Guid.NewGuid();
            userResult.Status = true;
            _userService.UpdateUserCustomer(userResult);
            return new SuccessResult(Message.UserActivated);
        }

        public IResult SendActivationToken(AppUser user)
        {
            _emailService.Send("KiralaMAX Hesap Aktivasyonu", "KiralaMAX'ın ayrıcalıklı dünyasına hoşgeldiniz. Aşağıdaki bağlantıdan hesabınızı " +
                "aktifleştirebilirsiniz. \n <a target='_blank' href='http://localhost:4200/activateaccount/" + $"{user.Email}/{ user.ActivationCode} >Hesabımı Aktifleştir</a>", user.Email);
            return new SuccessResult();

        }
    }
}
