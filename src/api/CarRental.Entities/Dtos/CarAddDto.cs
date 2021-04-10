using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CarRental.Entities.Dtos
{
    public class CarAddDto: IDto
    {

        public string Brand { get; set; }
        public string CarModel { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public int DailyPrice { get; set; }
        public int MinCreditScore { get; set; }
        public int CategoryId { get; set; }
        public string FuelType { get; set; }
        public int ModelYear { get; set; }
        public int EngineCapacity { get; set; }
        public IEnumerable<IFormFile> CarPhotos { get; set; }
    }
}
