using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.DbContexts.DbContextConfigurations
{
    public class CustomerConfigurations: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // customer user one to one relation
            builder.HasOne(u => u.AppUser).WithOne(c => c.Customer)
                .HasForeignKey<Customer>(x => x.AppUserId);
        }
    }
}
