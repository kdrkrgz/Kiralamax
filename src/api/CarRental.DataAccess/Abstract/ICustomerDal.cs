using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.DataAccess;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.DataAccess.Abstract
{
    public interface ICustomerDal: IEntityRepository<Customer>
    {
        List<CustomerDetailDto> GetAllCustomersWithDetails();
        CustomerDetailDto GetCustomerWithDetails(int id);
        List<CustomerBaseDto> GetCustomersBaseList();
        CustomerBaseDto GetCustomerByEmail(string email);
    }
}
