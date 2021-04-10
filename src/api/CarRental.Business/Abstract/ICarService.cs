using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CarRental.Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAllCarsWithDetails();
        IDataResult<Car> GetCarWithDetails(int id);

        IDataResult<Car> GetCar(int id);
        IDataResult<List<Car>> GetAllCars();
        IResult AddCar(Car car, List<string> carImages);
        IResult UpdateCar(Car car);
        IResult DeleteCar(Car car);
        IResult DeleteByCarId(int carId);
    }
}
