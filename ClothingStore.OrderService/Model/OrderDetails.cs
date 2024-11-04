namespace ClothingStore.OrderService.Model;

public class OrderDetails
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public Order? Order { get; set; }
}
