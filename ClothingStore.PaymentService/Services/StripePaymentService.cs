using ClothingStore.PaymentService.Interfaces;
using Stripe;

namespace ClothingStore.PaymentService.Services;

public class StripePaymentService : IPaymentService
{
    private readonly string _secretKey;

    public StripePaymentService(string secretKey)
    {
        _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey), "Stripe secret key cannot be null.");
        StripeConfiguration.ApiKey = _secretKey;
    }

    public async Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = currency,
        };

        var service = new PaymentIntentService();

        return await service.CreateAsync(options);
    }
}
