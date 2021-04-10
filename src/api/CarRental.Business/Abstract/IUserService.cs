using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<List<OperationClaim>> GetClaimsByUserId(int id);
        void Add(AppUser user);
        User GetByMail(string email);
        IDataResult<UserDataDto> GetUserDetails(string email);
        IDataResult<List<UserDataDto>> GetUsers();
        IResult UpdateUser(AppUser user, Customer customer);
        IResult UpdateUserClaims(UserClaimUpdateDto userClaimData);
        IResult UpdateUserCustomer(AppUser user);
        IResult DeleteUser(string userEmail);
    }
}
