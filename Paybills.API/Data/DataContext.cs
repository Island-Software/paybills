using System.Diagnostics.CodeAnalysis;
using Paybills.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Paybills.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}