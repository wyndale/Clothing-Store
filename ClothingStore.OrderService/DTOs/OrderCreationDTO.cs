namespace ClothingStore.OrderService.DTOs;

public class OrderCreationDTO
{
    public Guid UserId { get; set; } // ID of the user placing the order

    public List<OrderDetailCreationDTO> OrderDetails { get; set; } = new List<OrderDetailCreationDTO>();
}

public class OrderDetailCreationDTO
{
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}