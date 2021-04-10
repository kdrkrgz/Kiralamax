using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarRental.Core.Entities;

namespace CarRental.Core.Entities.Concrete
{
    public class User: IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        //public string Password { get; set; }
        public Guid ActivationCode { get; set; }

        // Authorization
        public bool Status { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
