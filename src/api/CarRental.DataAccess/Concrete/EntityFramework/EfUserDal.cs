using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.Core.DataAccess.EntityFramework;
using CarRental.Core.Entities.Concrete;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal: EfEntityRepositoryBase<CarRentalDbContext, AppUser>, IUserDal
    {
        private readonly CarRentalDbContext _context;
        public EfUserDal(CarRentalDbContext context) : base(context)
        {
            _context = context;
        }
        
        // Get User Claims (admin,editor, product.add etc...)
        public List<OperationClaim> GetClaims(User user)
        {
            return _context.UserOperationClaims
                .Include(u => u.User)
                .Include(oc => oc.OperationClaim)
                .AsNoTracking()
                .Where(x => x.UserId == user.Id)
                .Select(n => new OperationClaim
                {
                    id = n.OperationClaimId,
                    Name = n.OperationClaim.Name
                }).ToList();
        }



        public UserDataDto GetUserDetails(string email)
        {
           return _context.AppUsers
                .Include(c => c.Customer)
                .AsNoTracking()
                .Select(n => new UserDataDto
                {
                    Id   =  n.Id,
                    CustomerId = n.CustomerId,
                    CompanyName= n.Customer.CompanyName,
                    Email = n.Email,
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    Status = n.Status,
                }).FirstOrDefault(x => x.Email == email);
        }

        public List<UserDataDto> GetUsers()
        {
            return (_context.AppUsers
                    .Include(c => c.Customer)
                    .AsNoTracking()
                    .AsEnumerable() ?? throw new InvalidOperationException())
                .Select(n => new UserDataDto
                {
                    Id = n.Id,
                    Email = n.Email,
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    Status = n.Status,
                    CustomerId = n.Customer.Id,
                    CompanyName = n.Customer.CompanyName
      
                }).ToList();
        }
    }
}
