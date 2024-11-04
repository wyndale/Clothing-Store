using ClothingStore.ProductService.Configurations;
using ClothingStore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.ProductService.Data;

public class ProductServiceDbContext : DbContext
{
    public ProductServiceDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EntityConfiguration());
    }
}
