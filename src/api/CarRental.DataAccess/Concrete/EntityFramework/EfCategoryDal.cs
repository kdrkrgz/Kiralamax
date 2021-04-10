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
    public class EfCategoryDal: EfEntityRepositoryBase<CarRentalDbContext, Category>, ICategoryDal
    {
        private readonly CarRentalDbContext _context;
        public EfCategoryDal(CarRentalDbContext context) : base(context)
        {
            _context = context;
        }

        public Category GetCategoryWithoutCarData(int categoryId)
        {
            return _context.Categories.AsNoTracking().AsEnumerable().Select(x => new Category
            {
                Id = x.Id,
                CategoryName = x.CategoryName
            }).FirstOrDefault(x => x.Id == categoryId);
        }

    }
}
