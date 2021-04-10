using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.Core.DataAccess.EntityFramework;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfCarDal: EfEntityRepositoryBase<CarRentalDbContext, Car>, ICarDal
    {
        private readonly CarRentalDbContext _context;
        public EfCarDal(CarRentalDbContext context):base(context)
        {
            _context = context;
        }

        public List<Car> GetCarsWithDetails()
        {
            return _context.Cars
                .Include(r => r.Rentals)
                .Include(p => p.Photos)
                .Include(c => c.Category)
                .ToList();
        }

        public Car GetCarWithDetails(int id)
        {
            return _context.Cars
                .Include(r => r.Rentals)
                .Include(c => c.Category)
                .SingleOrDefault(x => x.Id == id);
        }

    }
}
