namespace ClothingStore.OrderService.Model;

public class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
