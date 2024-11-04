using ClothingStore.OrderService.Model;

namespace ClothingStore.OrderService.DTOs;

public class OrderResponseDTO
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
}

public class OrderDetailDTO
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
