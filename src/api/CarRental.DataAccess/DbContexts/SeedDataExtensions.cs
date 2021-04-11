using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Entities.Concrete;
using CarRental.Core.Utilities.Security.Hashing;
using CarRental.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.DbContexts
{
    public static class SeedDataExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Operation Claims
            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim { id = 1, Name = "admin" },
                new OperationClaim { id = 2, Name = "system.admin" },
                new OperationClaim { id = 3, Name = "customer" }
            );

            // App User Claims

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, CompanyName = "KiralaMAX", AppUserId = 1});
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    ActivationCode = Guid.NewGuid(),
                    CustomerId = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@kiralamax.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                });
            modelBuilder.Entity<UserOperationClaim>().HasData(
                new UserOperationClaim{id = 1, UserId = 1, OperationClaimId = 1},
                new UserOperationClaim{id = 2, UserId = 1, OperationClaimId = 2},
                new UserOperationClaim{id = 3, UserId = 1, OperationClaimId = 3}

            );



            //UserOperationClaim userOperationClaim1 = new UserOperationClaim()
            //{
            //    id = 1,
            //    OperationClaim = adminOperationClaim,
            //    User = appUser
            //};
            //UserOperationClaim userOperationClaim2 = new UserOperationClaim()
            //{
            //    id = 2,
            //    OperationClaim = systemAdminOperationClaim,
            //    User = appUser
            //};
            //UserOperationClaim userOperationClaim3 = new UserOperationClaim()
            //{
            //    id = 3,
            //    OperationClaim = customerOperationClaim,
            //    User = appUser
            //};

            //modelBuilder.Entity<UserOperationClaim>().HasData(
            //    userOperationClaim1,
            //    userOperationClaim2,
            //    userOperationClaim3
            //);

            //modelBuilder.Entity<AppUser>().HasData(
            //    new AppUser
            //    {
            //        ActivationCode = Guid.NewGuid(), 
            //        Customer = customer,
            //        FirstName = "Admin",
            //        LastName = "Admin",
            //        Email = "admin@kiralamax.com",
            //        PasswordHash = passwordHash,
            //        PasswordSalt = passwordSalt,
            //        Status = true,
            //        UserOperationClaims = 
            //    }
            //);
        }
    }
}
