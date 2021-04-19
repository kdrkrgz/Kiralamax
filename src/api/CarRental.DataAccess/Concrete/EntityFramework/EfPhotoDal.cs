using CarRental.Core.DataAccess.EntityFramework;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.DbContexts;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DataAccess.Concrete.EntityFramework
{
    public class EfPhotoDal: EfEntityRepositoryBase<CarRentalDbContext, Photo>, IPhotoDal
    {

        private readonly CarRentalDbContext _context;
        public EfPhotoDal(CarRentalDbContext context):base(context)
        {
            _context = context;
        }

        public void AddRange(List<Photo> carPhotos)
        {
            _context.CarPhotos.AddRange(carPhotos);
            _context.SaveChanges();
        }

        public void RemoveRange(List<Photo> carPhotos)
        {
            _context.CarPhotos.RemoveRange(carPhotos);
            _context.SaveChanges();
        }
    }
}
