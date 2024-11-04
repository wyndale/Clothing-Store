namespace ClothingStore.PaymentService.DTOs;

public class PaymentRequest
{
    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "usd";
}
