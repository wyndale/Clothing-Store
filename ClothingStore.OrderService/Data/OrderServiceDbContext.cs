using ClothingStore.OrderService.Configurations;
using ClothingStore.OrderService.Model;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.OrderService.Data;

public class OrderServiceDbContext : DbContext
{
    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetails> Details { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
