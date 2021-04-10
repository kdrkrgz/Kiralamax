using System.Collections.Generic;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<RentalDetailDto>> GetRentalsWithDetails();
        IDataResult<RentalDetailDto> GetRentalWithDetails(int id);

        IDataResult<Rental> GetRental(int id);
        IDataResult<List<Rental>> GetAllRentals();
        IResult AddRental(Rental rental);
        IResult UpdateRental(Rental rental);
        IResult DeleteRental(Rental rental);
        IResult DeleteRentalById(int rentalId);
        IResult AddRentalWithPayment(Rental rental, CreditCard creditCard, bool creditCardSaveStatus);
        IResult CompleteRentalByCarId(int carId);
        IResult AddTransactionalTest(Rental rental); // Transaction Management Test
    }
}
