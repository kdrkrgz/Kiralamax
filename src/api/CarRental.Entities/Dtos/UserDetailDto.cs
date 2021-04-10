using CarRental.Core.Entities;
using CarRental.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Entities.Dtos
{
    public class UserDetailDto: IDto
    {
        public int Id {get;set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Customer Customer {get;set;}
    }
}
