using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;
using CarRental.Core.Entities.Concrete;

namespace CarRental.Core.Entities.Concrete
{
    public class UserOperationClaim:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public OperationClaim OperationClaim { get; set; }
        public int OperationClaimId { get; set; }

    }
}
