using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class UserForLoginDto: IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
