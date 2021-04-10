using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class CustomerDetailDto: CustomerBaseDto, IDto
    {
        public string Email { get; set; }
        public int Rentals { get; set; }
        public bool IsRentalActive { get; set; }
    }
}
