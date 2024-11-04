using ClothingStore.PaymentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.PaymentService.Configurations;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(payment => payment.Amount).HasPrecision(10, 2);
    }
}
