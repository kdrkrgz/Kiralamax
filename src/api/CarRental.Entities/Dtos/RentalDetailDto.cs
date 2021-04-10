using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class RentalDetailDto: IDto
    {
        public int RentId { get; set; }
        public int CarId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public bool isFinished { get; set; }
        public decimal DailyPrice { get; set; }
        public int TotalRentDays { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
