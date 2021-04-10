using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.Core.DataAccess.EntityFramework;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal: EfEntityRepositoryBase<CarRentalDbContext, Customer>, ICustomerDal
    {
        private readonly CarRentalDbContext _context;
        public EfCustomerDal(CarRentalDbContext context) : base(context)
        {
            _context = context;
        }
        public List<CustomerDetailDto> GetAllCustomersWithDetails()
        {
            return (_context.Customers
                    .Include(u => u.AppUser)
                    .AsNoTracking()
                    .AsEnumerable() ?? throw new InvalidOperationException())
                .Select(x => new CustomerDetailDto
                {
                    CustomerId = x.Id,
                    CustomerName = x.AppUser.FirstName,
                    CustomerLastName = x.AppUser.LastName,
                    Company = x.CompanyName,
                    Email = x.AppUser.Email,
                    Rentals = x.Rentals.Count(r => r.CustomerId == x.Id)
                }).ToList();
        }

        public CustomerDetailDto GetCustomerWithDetails(int id)
        {
            return (_context.Customers
                .Include(u => u.AppUser)
                .AsNoTracking()
                .AsEnumerable() ?? throw new InvalidOperationException())
                .Select(x => new CustomerDetailDto
                {
                    CustomerId = x.Id,
                    CustomerName = x.AppUser.FirstName,
                    CustomerLastName = x.AppUser.LastName,
                    Company = x.CompanyName,
                    Email = x.AppUser.Email,
                    Rentals = x.Rentals.Count(r => r.CustomerId == x.Id),
                    IsRentalActive = x.Rentals.Any()
                })
                .SingleOrDefault(x => x.CustomerId == id);
        }

        public List<CustomerBaseDto> GetCustomersBaseList()
        {
            return (_context.Customers
                    .Include(a => a.AppUser)
                    .Include(c => c.CustomerCards)
                    .AsNoTracking()
                    .AsEnumerable() ?? throw new InvalidOperationException())
                .Select(x => new CustomerBaseDto
                {
                    CustomerId = x.Id,
                    CustomerName = x.AppUser.FirstName,
                    CustomerLastName = x.AppUser.LastName,
                    Company = x.CompanyName,
                }).ToList();
        }

        public CustomerBaseDto GetCustomerByEmail(string email)
        {
            return _context.Customers
                    .Include(a => a.AppUser)
                    .Include(c => c.CustomerCards)
                    .AsNoTracking()
                    .AsEnumerable() // TODO
                .Select(x => new CustomerBaseDto
                {
                    CustomerId = x.Id,
                    CustomerName = x.AppUser.FirstName,
                    CustomerLastName = x.AppUser.LastName,
                    Company = x.CompanyName,
                    CustomerEmail= x.AppUser.Email,
                }).FirstOrDefault(x => x.CustomerEmail == email);
        }
    }
}
