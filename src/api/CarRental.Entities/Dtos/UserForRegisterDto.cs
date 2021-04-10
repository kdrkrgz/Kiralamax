using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Entities.Dtos
{
    public class UserForRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
