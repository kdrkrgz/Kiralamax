using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;
using CarRental.Core.Entities.Concrete;

namespace CarRental.Entities.Entities
{
    [NotMapped]
    public class AppUser: User
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
