using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;
using CarRental.Entities.Entities;

namespace CarRental.Entities.Dtos
{
    public class CustomerBaseDto: IDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string Company { get; set; }
        public string CustomerEmail { get; set; }
    }
}
