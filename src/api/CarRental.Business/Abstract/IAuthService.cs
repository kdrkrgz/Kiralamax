using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Core.Utilities.Security.JWT;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string userEmail);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ForgotPassword(string userEmail);
        IResult ResetPassword(PasswordResetDto passwordResetDto);
        IResult SendActivationToken(AppUser user);
        IResult ActivateUser(AccountActivateDto accountActivateDto);
    }
}
