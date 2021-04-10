using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class AccountActivateDto: IDto
    {
        public string UserEmail { get; set; }
        public Guid Code { get; set; }
    }
}
