using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess;
using CarRental.Entities.Entities;

namespace CarRental.DataAccess.Abstract
{
    public interface ICategoryDal: IEntityRepository<Category>
    {
        Category GetCategoryWithoutCarData(int categoryId);
    }
}
