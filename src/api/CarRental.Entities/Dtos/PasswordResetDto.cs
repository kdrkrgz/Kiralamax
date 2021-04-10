using CarRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Entities.Dtos
{
    public class PasswordResetDto: AccountActivateDto
    {
        public string Password { get; set; }
    }
}
