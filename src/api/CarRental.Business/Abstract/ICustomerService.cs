using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<CustomerDetailDto>> GetCustomersWithDetails();
        IDataResult<CustomerDetailDto> GetCustomerWithDetails(int id);
        IDataResult<List<CustomerBaseDto>> GetCustomerBaseList();
        IDataResult<Customer> GetCustomerByUserId(int userId);

        IDataResult<Customer> GetCustomer(int id);
        IDataResult<List<Customer>> GetAllCustomers();
        IDataResult<CustomerBaseDto> GetCustomerWithEmail(string email);
        IResult AddCustomer(Customer customer);
        IResult UpdateCustomer(Customer customer);
        IResult DeleteCustomer(Customer customer);
    }
}
