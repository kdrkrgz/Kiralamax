using CarRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Entities.Dtos
{
    public class CreditCardDto: IDto
    {
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CvvCode { get; set; }
    }
}
