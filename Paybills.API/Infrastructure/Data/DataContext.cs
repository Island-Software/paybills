using System.Diagnostics.CodeAnalysis;
using Paybills.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Paybills.API.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            var pgHost = Environment.GetEnvironmentVariable("PG_HOST");
            var pgPort = Environment.GetEnvironmentVariable("PG_PORT");
            var pgUser = Environment.GetEnvironmentVariable("PG_USER");
            var pgPassword = Environment.GetEnvironmentVariable("PG_PASSWORD");
            var pgDb = Environment.GetEnvironmentVariable("PG_DB");

            var connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPassword};Database={pgDb};SSL Mode=Disable";

            optionsBuilder.UseNpgsql(connStr);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}