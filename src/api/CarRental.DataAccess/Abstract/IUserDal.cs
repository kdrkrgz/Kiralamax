using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess;
using CarRental.Core.Entities.Concrete;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<AppUser>
    {
        List<OperationClaim> GetClaims(User user);
        UserDataDto GetUserDetails(string email);
        List<UserDataDto> GetUsers();
    }
}
