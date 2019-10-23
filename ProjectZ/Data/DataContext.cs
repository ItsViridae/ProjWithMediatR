using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectZ.Data.Entities;
using Vinformatix.Api.Data;
using Vinformatix.EntityFrameworkCore;
using Vinformatix.Security;

namespace ProjectZ.Data
{
    public class DataContext : DataContextBase
    {
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyConfigurations(builder);
            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHash("admin");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    FirstName = "Seeded-FirstName",
                    LastName = "Seeded-LastName",
                    PasswordHash = passwordHasher.Hash,
                    PasswordSalt = passwordHasher.Salt
                });

            builder.Entity<Association>().HasData(
                new Association
                {
                    Id = 7,
                    Name = "Seven"
                });
        }
    }
}
