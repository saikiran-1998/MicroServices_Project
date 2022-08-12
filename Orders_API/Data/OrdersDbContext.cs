using Microsoft.EntityFrameworkCore;

namespace Orders_API.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        //  public DbSet<OrderItem> OrderItems { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderItem>().HasNoKey();
        //}

    }
}
