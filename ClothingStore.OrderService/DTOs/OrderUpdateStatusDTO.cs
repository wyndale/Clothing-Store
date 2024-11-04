using ClothingStore.OrderService.Model;

namespace ClothingStore.OrderService.DTOs;

public class OrderUpdateStatusDTO
{
    public string Status { get; set; } = "Pending";
}
