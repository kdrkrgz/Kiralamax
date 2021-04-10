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
using Microsoft.AspNetCore.Http;

namespace CarRental.Business.Concrete
{
    public class CarManager: ICarService
    {

        private readonly ICarDal _carDal;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public CarManager(ICarDal carDal, ICategoryService categoryService, IPhotoService photoService)
        {
            _carDal = carDal;
            _categoryService = categoryService;
            _photoService = photoService;
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Car>> GetAllCarsWithDetails()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetCarsWithDetails());
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<Car> GetCarWithDetails(int id)
        {
            return new SuccessDataResult<Car>(_carDal.GetCarWithDetails(id));
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<Car> GetCar(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Car>> GetAllCars()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult AddCar(Car car, List<string> carImages)
        {
            var category = GetAddedCarCategory(car.CategoryId);
            car.CategoryId = category.Data.Id;
            car.Category = category.Data;
            //TODO photo tarafına addrangeli add metod // ve geri dön photolist liste şeklinde
            _carDal.Add(car);

            AddPhotoToCar(carImages, car.Brand + " " + car.CarModel, car); // böyle bir araç yoking hatası fırlatacak kesin
            return new SuccessResult(Message.CarAdded);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult UpdateCar(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Message.CarRentUpdated);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteCar(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Message.CarDeleted);
        }

        [SecuredOperation("admin,car.add")]
        private IDataResult<Category> GetAddedCarCategory(int categoryId)
        {
            // get category from request id with carmodel
            return _categoryService.GetCategory(categoryId);
        }

        private IResult AddPhotoToCar(List<string> photoPaths, string description, Car car)
        {
            List<Photo> carPhotos = new List<Photo>();
            foreach (var photoPath in photoPaths)
            {
                Photo carPhoto = new Photo
                {
                    Path = photoPath,
                    Description = description,
                    Car = car
                };
                carPhotos.Add(carPhoto);
            }
            _photoService.AddCarPhotos(carPhotos);

            return new SuccessResult();

        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteByCarId(int carId)
        {
            var result = GetCar(carId);
            if (result.IsSuccess)
            {
                DeleteCar(result.Data);
                return new SuccessResult(Message.CarDeleted);
            }
            return new ErrorResult(Message.CarDeleteFailed);
        }
    }
}
