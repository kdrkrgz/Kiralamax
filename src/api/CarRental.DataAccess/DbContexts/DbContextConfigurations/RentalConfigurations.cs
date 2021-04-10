using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.DbContexts.DbContextConfigurations
{
    public class RentalConfigurations: IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            // customer car many to many relation
            builder
                .HasOne<Car>(c => c.Car)
                .WithMany(r => r.Rentals)
                .HasForeignKey(f => f.CarId);

            builder
                .HasOne<Customer>(c => c.Customer)
                .WithMany(r => r.Rentals)
                .HasForeignKey(f => f.CustomerId);
        }
    }
}
