using CarRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarRental.Entities.Entities
{
    public class CreditCard: IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity) ]
        public int Id { get; set; }
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CvvCode { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
