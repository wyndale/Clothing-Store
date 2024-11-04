namespace ClothingStore.PaymentService.Models;

public class Payment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string Currency {  get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public PaymentStatus Status { get; set; }

    public required string PaymentProvider { get; set; }

    public string TransactionId { get; set; } = string.Empty;
}
