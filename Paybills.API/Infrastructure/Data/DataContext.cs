using Paybills.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Paybills.API.Domain.Entities;

namespace Paybills.API.Data
{
    public class DataContext : DbContext
    {
        private IHostEnvironment _env;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext(DbContextOptions<DataContext> options, IHostEnvironment env) : base(options)
        {
            _env = env;
        }

        public DataContext()
        {
        }

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
        public DbSet<ReceivingType> ReceivingTypes { get; set; }
        public DbSet<Receiving> Receivings { get; set; }

    }
}