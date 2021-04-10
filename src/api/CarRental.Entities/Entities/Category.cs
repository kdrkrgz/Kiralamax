using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;


namespace CarRental.Entities.Entities
{
    public class Category: IEntity
    {
        public Category()
        {
            Cars = new Collection<Car>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
