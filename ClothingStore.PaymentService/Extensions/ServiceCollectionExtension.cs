using ClothingStore.PaymentService.Interfaces;
using ClothingStore.PaymentService.Services;

namespace ClothingStore.PaymentService.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration["Stripe:SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new ArgumentNullException(nameof(secretKey), "Stripe secret key is missing from configuration.");
        }

        services.AddScoped<IPaymentService>(provider => new StripePaymentService(secretKey));

        return services;
    }
}
