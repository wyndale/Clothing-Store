using System.ComponentModel.DataAnnotations;

namespace ClothingStore.ProductService.Models;

public class Inventory
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Stock { get; set; }

    public Product? Product { get; set; }
}
