using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        //Creating the DataSet for the Tables
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}
