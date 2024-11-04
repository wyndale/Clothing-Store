using ClothingStore.ProductService.Data;
using ClothingStore.ProductService.DTOs;
using ClothingStore.ProductService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.ProductService.Controllers;

public class InventoryController : BaseController
{
    private readonly ProductServiceDbContext _context;

    public InventoryController(ProductServiceDbContext context)
    {
        _context = context;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("list-of-inventories")]
    public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetInventories()
    {
        var inventory = await _context.Inventories
            .Include(p => p.Product)
            .Select(p => new InventoryDTO
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Stock = p.Stock,
                ProductName = p.Product != null ? p.Product.Name : null
            })
            .ToListAsync();

        return Ok(inventory);
    }

    [HttpGet("get/{id}/inventory")]
    public async Task<ActionResult<InventoryDTO>> GetInventoryById(Guid id)
    {
        var inventory = await _context.Inventories
            .Include(p => p.Product)
            .Where(p => p.Id == id)
            .Select(p => new InventoryDTO
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Stock = p.Stock,
                ProductName = p.Product != null ? p.Product.Name : null
            })
            .FirstOrDefaultAsync();

        if (inventory == null) return NotFound();

        return Ok(inventory);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("new-inventory")]
    public async Task<ActionResult<InventoryDTO>> AddInventory(AddInventoryDTO inventoryDTO)
    {
        var inventory = new Inventory
        {
            Id = Guid.NewGuid(),
            ProductId = inventoryDTO.ProductId,
            Stock = inventoryDTO.Stock
        };

        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        var newCreatedInventory = await _context.Inventories
            .Include(p => p.Product)
            .Where(p => p.Id == inventory.Id)
            .Select(p => new InventoryDTO
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Stock = p.Stock,
                ProductName = p.Product != null ? p.Product.Name : null
            })
            .FirstOrDefaultAsync();

        if (newCreatedInventory == null) return NoContent();

        return CreatedAtAction(nameof(GetInventoryById), new { id = newCreatedInventory.Id }, newCreatedInventory);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("update/{id}/inventory")]
    public async Task<ActionResult> UpdateInventory(Guid id, UpdateInventoryDTO inventoryDTO)
    {
        var inventory = await _context.Inventories.FindAsync(id);

        if (inventory == null) return NotFound();

        inventory.Stock = inventoryDTO.Stock;
        inventory.ProductId = inventoryDTO.ProductId;

        _context.Entry(inventory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InventoryExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete/{id}/inventory")]
    public async Task<ActionResult> DeleteInventory(Guid id)
    {
        var inventory = await _context.Inventories.FindAsync(id);

        if (inventory == null) return NotFound();

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool InventoryExists(Guid id)
    {
        return _context.Inventories.Any(e => e.Id == id);
    }
}
