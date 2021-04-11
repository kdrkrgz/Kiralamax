using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.DataAccess.DbContexts.DbContextConfigurations;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRental.DataAccess.DbContexts
{
    public class CarRentalDbContext: DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options):base(options)
        {
            
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Photo> CarPhotos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RentalConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerConfigurations());
            modelBuilder.ApplyConfiguration(new UserOperationClaimConfigurations());

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
