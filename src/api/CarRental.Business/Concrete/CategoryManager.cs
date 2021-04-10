using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Entities;

namespace CarRental.Business.Concrete
{
    public class CategoryManager: ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult AddCategory(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Message.CategoryAdded);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult DeleteCategoryById(int id)
        {
            var category = _categoryDal.Get(x => x.Id == id);
            if(category != null)
            {
                _categoryDal.Delete(category);
                return new SuccessResult(Message.CategoryDeleted);
            }
            return new ErrorResult(Message.CategoryDeleteFailed);
            
        }

        [TransactionScopeAspect]
        [CacheAspect]
        public IDataResult<List<Category>> GetCategories()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Category> GetCategory(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.Id == id));
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult UpdateCategory(Category category)
        {

            var result = GetCategory(category.Id);
            result.Data.CategoryName = category.CategoryName;
            _categoryDal.Update(result.Data);
            return new SuccessResult(Message.CategoryUpdated);
        }
    }
}
