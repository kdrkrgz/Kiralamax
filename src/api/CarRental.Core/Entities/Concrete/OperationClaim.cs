using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Core.Entities.Concrete
{
    public class OperationClaim: IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string Name { get; set; }

        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
