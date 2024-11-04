using ClothingStore.OrderService.Data;
using ClothingStore.OrderService.DTOs;
using ClothingStore.OrderService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.OrderService.Controllers;

public class OrdersController : BaseController
{
    private readonly OrderServiceDbContext _context;

    public OrdersController(OrderServiceDbContext context)
    {
        _context = context;
    }

    // Place a new order
    [HttpPost("place-order")]
    public async Task<ActionResult<OrderResponseDTO>> PlaceOrder(OrderCreationDTO orderDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending,
                OrderDetails = orderDto.OrderDetails.Select(d => new OrderDetails
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            };

            order.TotalAmount = order.OrderDetails.Sum(d => d.Price * d.Quantity);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Map to response DTO
            var orderResponse = new OrderResponseDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.OrderStatus,
                OrderDetails = order.OrderDetails.Select(d => new OrderDetailDTO
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderResponse);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    // Retrieve order history for a specific user
    [HttpGet("user/{userId}/history")]
    public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetOrderHistory(Guid userId)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        var orderResponses = orders.Select(order => new OrderResponseDTO
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.OrderStatus,
            OrderDetails = order.OrderDetails.Select(d => new OrderDetailDTO
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                Price = d.Price
            }).ToList()
        }).ToList();

        return orderResponses;
    }

    // Delete order history for a specific user
    [HttpDelete("{userId}/delete-order-history")]
    public async Task<ActionResult> DeleteOrderHistory(Guid userId)
    {
        var userOrders = _context.Orders.Where(o => o.UserId == userId).ToList();

        if (!userOrders.Any())
        {
            return NotFound();
        }

        _context.Orders.RemoveRange(userOrders);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Delete a specific order by orderId for a specific user
    [HttpDelete("{userId}/delete-order/{orderId}/history")]
    public async Task<ActionResult> DeleteSpecificOrder(Guid userId, Guid orderId)
    {
        var order = await _context.Orders
            .Where(o => o.UserId == userId && o.Id == orderId)
            .FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }



    // Update the status of an order
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("{id}/order-status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, OrderUpdateStatusDTO statusDto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        // Try parsing the status string to the enum
        if (!Enum.TryParse<OrderStatus>(statusDto.Status, true, out var parsedStatus))
        {
            // Handle integer conversion, if Status is provided as a numeric value
            if (int.TryParse(statusDto.Status, out int statusInt) && Enum.IsDefined(typeof(OrderStatus), statusInt))
            {
                parsedStatus = (OrderStatus)statusInt;
            }
            else
            {
                return BadRequest("Invalid order status.");
            }
        }

        order.OrderStatus = parsedStatus;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id}/view-order")]
    public async Task<ActionResult<OrderResponseDTO>> GetOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var orderResponse = new OrderResponseDTO
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.OrderStatus,
            OrderDetails = order.OrderDetails.Select(d => new OrderDetailDTO
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                Price = d.Price
            }).ToList()
        };

        return orderResponse;
    }

}
