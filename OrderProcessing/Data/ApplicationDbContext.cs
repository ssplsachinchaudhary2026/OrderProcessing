using Microsoft.EntityFrameworkCore;
using OrderProcessing.Models;

namespace OrderProcessing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.Total).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.DiscountAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().HasData(
                 new Product { Id = 1, Name = "Laptop", Price = 55000, Stock = 10 },
                new Product { Id = 2, Name = "Mouse", Price = 700, Stock = 50 },
                new Product { Id = 3, Name = "Keyboard", Price = 1500, Stock = 30 },
                new Product { Id = 4, Name = "Monitor", Price = 12000, Stock = 15 }
            );

        }
    }
}
