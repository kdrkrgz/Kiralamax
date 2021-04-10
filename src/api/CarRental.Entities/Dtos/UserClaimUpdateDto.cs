using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Dtos
{
    public class UserClaimUpdateDto: IDto
    {
        public string Useremail { get; set; }
        public int[]  Claimids { get; set; }
    }
}
