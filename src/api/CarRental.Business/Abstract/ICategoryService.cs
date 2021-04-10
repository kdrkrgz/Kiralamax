using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(Category category);
        IDataResult<Category> GetCategory(int id);
        IDataResult<List<Category>> GetCategories();
        IResult DeleteCategoryById(int id);
        IResult UpdateCategory(Category category);
    }
}
