using Stripe;

namespace ClothingStore.PaymentService.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency);
}