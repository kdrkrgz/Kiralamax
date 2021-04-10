using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class UserDataDto: IDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
    }
}
