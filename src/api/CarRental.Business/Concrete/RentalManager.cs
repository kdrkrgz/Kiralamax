using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Transactions;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Business.ValidationRules.FluentValidation;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Performance;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.Aspects.Autofac.Validation;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Business;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using log4net.DateFormatter;

namespace CarRental.Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly ICreditCardService _creditCardService;
        public RentalManager(IRentalDal rentalDal, ICarService carService, ICustomerService customerService, ICreditCardService creditCardService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
            _creditCardService = creditCardService;

        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<RentalDetailDto>> GetRentalsWithDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsWithDetails());
        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<RentalDetailDto> GetRentalWithDetails(int id)
        {
            throw new Exception("hata testi");
            //return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalWithDetails(id));
        }

        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        public IDataResult<Rental> GetRental(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == id));
        }


        [CacheAspect]
        [SecuredOperation("admin,system.admin")]
        public IDataResult<List<Rental>> GetAllRentals()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(RentalValidation))]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult AddRental(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Message.CarRentAdded);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(RentalValidation))]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult UpdateRental(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Message.CarRentUpdated);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult DeleteRental(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Message.CarRentDeleted);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult DeleteRentalById(int rentalId)
        {
            var result = GetRental(rentalId);
            if (result.IsSuccess)
            {
                DeleteRental(result.Data);
                UpdateCarIsRentedFalse(result.Data.CarId);
                return new SuccessResult(Message.CarRentDeleted);
            }
            return new ErrorResult(Message.CarRentDeleteFailed);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult CompleteRentalByCarId(int carId)
        {
            var result = _carService.GetCarWithDetails(carId);
            if (result.IsSuccess)
            {
                var carRentals = result.Data.Rentals.FirstOrDefault(x => x.IsFinished == false);
                carRentals.IsFinished = true;
                carRentals.ReturnDate = DateTime.Now;
                result.Data.IsRented = false;
                _carService.UpdateCar(result.Data);
                return new SuccessResult(Message.CarRentComplated);
            }
            return new ErrorResult(Message.CarRentComplateFailed);


            //var result = GetRentalWithAll(rentalId);
            //if (result.IsSuccess)
            //{
            //    result.Data.IsFinished = true;
            //    result.Data.ReturnDate = DateTime.Now;
            //    // TODO: cannot be tracked because another instance with the same key value for {'CarId', 'CustomerId'} is already being tracked.
            //    UpdateRental(result.Data);
            //    //UpdateCarIsRentedFalse(result.Data.CarId);
            //    return new SuccessResult(Message.CarRentDeleted);
            //}
            //return new ErrorResult(Message.CarRentDeleteFailed);
        }

        [TransactionScopeAspect]
        public IResult AddRentalWithPayment(Rental rental, CreditCard creditCard, bool creditCardSaveStatus)
        {
            var carResult= _carService.GetCar(rental.CarId);
            var customerResult = _customerService.GetCustomer(rental.CustomerId);
            rental.RentDate = Convert.ToDateTime(rental.RentDate);
            rental.ReturnDate = Convert.ToDateTime(rental.ReturnDate);

            rental.Car = carResult.Data;
            rental.CarId = carResult.Data.Id;

            rental.Customer = customerResult.Data;
            rental.CustomerId = customerResult.Data.Id;
            
            if (creditCardSaveStatus)
            {
               creditCard.Customer = customerResult.Data;
              _creditCardService.AddCreditCard(creditCard);
            }

            var result = AddRental(rental);
            if (result.IsSuccess)
            {
                UpdateCarIsRentedTrue(rental.CarId);
                return new SuccessResult(Message.CarRentAdded);
            }

            return new ErrorResult(Message.CarRentAddFailed);
        }

        // Business codes

        private IResult UpdateCarIsRentedTrue(int carId)
        {
            var carResult = _carService.GetCar(carId);
            carResult.Data.IsRented = true;
            var result = _carService.UpdateCar(carResult.Data);
            if (result.IsSuccess)
            {
                return new SuccessResult(Message.CarRentUpdated);
            }
            return new ErrorResult(Message.CarRentUpdateFailed);
        }
        
        private IResult UpdateCarIsRentedFalse(int carId)
        {
            var carResult = _carService.GetCar(carId);
            carResult.Data.IsRented = false;
            var result = _carService.UpdateCar(carResult.Data);
            if (result.IsSuccess)
            {
                return new SuccessResult(Message.CarRentDeleted);
            }
            return new ErrorResult(Message.CarRentDeleteFailed);
        }

        //private IResult CheckRentalCount(int carId)
        //{
            // bir araç en fazla 1 kere kiralanabilir. bu hem update kısmında hem add kısmında kullanılabileceği için 
            // DRY felsefesine uygun değildir. dolayısıyla clean code anlayışıyla ilgili business kodu metoda alarak 
            // hem add hem update metodları içinde kullanabilecek hale getirdik.

            // select count(*) from Rental where carid = 1 gibi çalışır. => linqtoSql => linq komutlarını sql komutlarına çevirerek çalıştırır.
            //* if (_rentalDal.GetAll(x => x.CarId == carId).Any())
            //*    return new ErrorResult(Message.RentalCountError);

        //    return new SuccessResult();
        //}

        // Transaction Yönetimi: Banka hesabından birine para gönderdiğinizde çalışan metodu düşünün önce sizin paranızı x birim eksiltecek daha sonra 
        // gönderdiğiniz kişinin parasını x birim artıracak olması gerekit normal şartlarda ancak bir hatadan dolayı x birim para gönderilenin hesabına eklenemezse
        // metodun başında gönderen kişinin x birim parasının silinmemesi gerekir yani bir nevi işlemin iptal edilmesi gerekir.
        // aşağıdaki metodda bu işlem yapılabilir ancak bu clean code olmayacağı için [TransactionScopeAspect] gibi bir aspectle sarmallamayı yapabiliriz.
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Rental rental)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    AddRental(rental);
                    if ((rental.ReturnDate - rental.RentDate).TotalDays < 2)
                    {
                        throw new Exception("kiralama en az 2 gün yapılabilir.");
                    }
                    AddRental(rental);
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
            return null;
        }

        
    }
}
