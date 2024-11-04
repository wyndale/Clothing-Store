namespace ClothingStore.ProductService.DTOs;

public class InventoryDTO
{
    public Guid Id { get; set; }
    public int Stock { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
}

public record AddInventoryDTO(
    int Stock,
    Guid ProductId
    );

public record UpdateInventoryDTO(
    int Stock,
    Guid ProductId
    );