using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Models;

namespace OnlineShopping.Data
{
    class OnlineShoppingDbContext : DbContext
    {
        public OnlineShoppingDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(c => new { c.OrderId, c.ProductId });
        }
    }
}
