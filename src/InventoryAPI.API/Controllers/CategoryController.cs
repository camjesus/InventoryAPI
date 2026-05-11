using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.API.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<Ok<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return TypedResults.Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<Results<Ok<CategoryResponseDto>, NotFound>> GetById(Guid id)
    {
        var category = await _service.GetByIdAsync(id);
        return TypedResults.Ok(category);
    }

    [HttpPost]
    public async Task<Created<CategoryResponseDto>> Create([FromBody] CreateCategoryDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return TypedResults.Created($"/api/category/{created.Id}", created);
    }

    [HttpPut("{id:guid}")]
    public async Task<Results<Ok<CategoryResponseDto>, NotFound>> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return TypedResults.Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<Results<NoContent, NotFound>> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return TypedResults.NoContent();
    }
}