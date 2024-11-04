namespace ClothingStore.ProductService.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

public record AddProductDTO(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId
    );

public record UpdateProductDTO(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId
    );