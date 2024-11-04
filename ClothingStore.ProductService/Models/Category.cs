using System.ComponentModel.DataAnnotations;

namespace ClothingStore.ProductService.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new List<Product>();
}
