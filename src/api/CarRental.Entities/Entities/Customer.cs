using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Entities
{
    public class Customer: IEntity
    {
        public Customer()
        {
            Rentals = new Collection<Rental>();
            CustomerCards = new Collection<CreditCard>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        #nullable enable
        public ICollection<CreditCard> CustomerCards { get; set; }
        #nullable disable
    }
}
