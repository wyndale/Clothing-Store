using ClothingStore.ProductService.Data;
using ClothingStore.ProductService.DTOs;
using ClothingStore.ProductService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.ProductService.Controllers;

public class ProductsController : BaseController
{
    private readonly ProductServiceDbContext _context;

    public ProductsController(ProductServiceDbContext context)
    {
        _context = context;
    }

    [HttpGet("list-of-products")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProduct()
    {
        var product = await _context.Products
            .Include(p => p.Categories)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Categories != null ? p.Categories.Name : null
            })
            .ToListAsync();

        return Ok(product);
    }

    [HttpGet("get/{id}/product")]
    public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Categories)
            .Where(p => p.Id == id)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                 CategoryName = p.Categories != null ? p.Categories.Name : null
            })
            .FirstOrDefaultAsync();

        if (product == null) return NotFound();

        return Ok(product);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("new-product")]
    public async Task<ActionResult<ProductDTO>> AddProduct(AddProductDTO product)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId
        };

        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();

        var createdProduct = await _context.Products
            .Include(p => p.Categories)
            .Where(p => p.Id == newProduct.Id)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Categories != null ? p.Categories.Name : null
            })
            .FirstOrDefaultAsync();

        if (createdProduct == null) return NoContent();

        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("update/{id}/product")]
    public async Task<ActionResult> UpdateProduct(UpdateProductDTO updateProduct, Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return NotFound();

        product.Name = updateProduct.Name;
        product.Description = updateProduct.Description;
        product.Price = updateProduct.Price;
        product.CategoryId = updateProduct.CategoryId;

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete/{id}/product")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return NoContent();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(Guid id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
