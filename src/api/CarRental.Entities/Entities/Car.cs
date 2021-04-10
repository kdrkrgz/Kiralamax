using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Schema;
using CarRental.Core.Entities;

namespace CarRental.Entities.Entities
{
    public class Car: IEntity
    {
        public Car()
        {
            Rentals = new Collection<Rental>();
            Photos = new Collection<Photo>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Brand { get; set; }

        public string CarModel { get; set; }

        public string Color { get; set; }

        public string Gear { get; set; }

        public int DailyPrice { get; set; }

        public string FuelType { get; set; }

        public bool IsRented { get; set; }

        public int CategoryId { get; set; }

        public int ModelYear { get; set; }

        public int EngineCapacity { get; set; }

        public int MinCreditScore { get; set; }

        public Category Category { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
        public IEnumerable<Photo> Photos {get;set;}

    }
}
