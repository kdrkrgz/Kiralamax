using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CarRental.Core.DataAccess.EntityFramework;
using CarRental.Core.Utilities;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal: EfEntityRepositoryBase<CarRentalDbContext, Rental>, IRentalDal
    {
        private readonly CarRentalDbContext _context;
        public EfRentalDal(CarRentalDbContext context) : base(context)
        {
            _context = context;
        }
        public List<RentalDetailDto> GetRentalsWithDetails()
        {
            /// Include or select new performance
            var result = (_context.Rentals
                    .Include(c => c.Car)
                    .Include(cu => cu.Customer)
                    .ThenInclude(u => u.AppUser)
                    .AsNoTracking()
                    .AsEnumerable() ?? throw new InvalidOperationException())
                .Select(x => new RentalDetailDto
                {
                    RentId = x.Id,
                    CarId = x.Car.Id,
                    RentDate = x.RentDate,
                    ReturnDate = x.ReturnDate,
                    CustomerName = x.Customer.AppUser.FirstName,
                    CustomerLastName =  x.Customer.AppUser.LastName,
                    CarBrand = x.Car.Brand,
                    CarModel = x.Car.CarModel,
                    isFinished = x.IsFinished,
                    DailyPrice = x.Car.DailyPrice,
                    TotalRentDays = (int)(x.ReturnDate - x.RentDate).TotalDays ,
                    TotalPrice = (((int)(x.ReturnDate - x.RentDate).TotalDays) * x.Car.DailyPrice),
                })
                .ToList();

            return result;

        }

        public RentalDetailDto GetRentalWithDetails(int id)
        {
             
            var result= (_context.Rentals
                    .Include(c => c.Car)
                    .Include(cu => cu.Customer)
                    .ThenInclude(u => u.AppUser)
                    .AsNoTracking()
                    .AsEnumerable() ?? throw new InvalidOperationException())
                .Select( x => new RentalDetailDto
                {
                    RentId = x.Id,
                    CarId = x.Car.Id,
                    RentDate = x.RentDate,
                    ReturnDate = x.ReturnDate,
                    CustomerName = x.Customer.AppUser.FirstName,
                    CustomerLastName = x.Customer.AppUser.LastName,
                    CarBrand = x.Car.Brand,
                    CarModel = x.Car.CarModel,
                    isFinished = x.IsFinished,
                    DailyPrice = x.Car.DailyPrice,
                    TotalRentDays = (int)(x.ReturnDate - x.RentDate).TotalDays ,
                    TotalPrice = (((int)(x.ReturnDate - x.RentDate).TotalDays) * x.Car.DailyPrice),
                })
                .SingleOrDefault(r => r.RentId == id);

            return result;
        }

        public Rental GetRentalWithAll(int id)
        {
            return (_context.Rentals
                .Include(c => c.Car)
                .Include(cu => cu.Customer)
                .ThenInclude(u => u.AppUser)
                .AsNoTracking()
                .AsEnumerable() ?? throw new InvalidOperationException())
                .SingleOrDefault(r => r.Id == id);
        }
    }
}
