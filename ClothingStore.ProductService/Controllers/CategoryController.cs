using ClothingStore.ProductService.Data;
using ClothingStore.ProductService.DTOs;
using ClothingStore.ProductService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.ProductService.Controllers;

public class CategoryController : BaseController
{
    private readonly ProductServiceDbContext _context;

    public CategoryController(ProductServiceDbContext context)
    {
        _context = context;
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return Ok(categories);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("add-category")]
    public async Task<ActionResult<CategoryDTO>> AddCategory(AddCategoryDTO categoryDTO)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = categoryDTO.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };

        return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, result);
    }
}
