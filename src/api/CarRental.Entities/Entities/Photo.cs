using CarRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarRental.Entities.Entities
{
    public class Photo : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsCover { get; set; }

        public int CarId { get; set; }
        public virtual Car Car {get;set;}
    }
}
