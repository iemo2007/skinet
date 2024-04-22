using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductConfiguration(modelBuilder.Entity<Product>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
