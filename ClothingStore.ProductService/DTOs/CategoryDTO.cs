namespace ClothingStore.ProductService.DTOs;

public class CategoryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public record AddCategoryDTO(string Name);