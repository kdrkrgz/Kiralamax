using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess;
using CarRental.Core.Utilities;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.DataAccess.Abstract
{
    public interface IRentalDal: IEntityRepository<Rental>
    {

        List<RentalDetailDto> GetRentalsWithDetails();
        RentalDetailDto GetRentalWithDetails(int id);
        Rental GetRentalWithAll(int id);

    }
}
