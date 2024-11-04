using ClothingStore.PaymentService.Configurations;
using ClothingStore.PaymentService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.PaymentService.Data;

public class PaymentServiceDbContext : DbContext
{
    public PaymentServiceDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PaymentTransactionConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
