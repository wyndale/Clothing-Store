using System.ComponentModel.DataAnnotations;

namespace ClothingStore.ProductService.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public Category? Categories { get; set; }
}
