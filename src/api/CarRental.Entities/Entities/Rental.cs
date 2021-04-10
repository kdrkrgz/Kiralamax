using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Entities.Entities
{
    public class Rental: IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public bool IsFinished { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
