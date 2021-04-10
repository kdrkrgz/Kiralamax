using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.DbContexts.DbContextConfigurations
{
    public class UserOperationClaimConfigurations: IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            // Authentication Db scheme useroperationclaims contains 2 fk for many to many relation (userid, operationid)
            builder.HasKey(fk => new { fk.UserId, fk.OperationClaimId });
        }
    }
}
