using InventoryAPI.Domain.DTOs.Product;
using InventoryAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.API.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<Ok<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return TypedResults.Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<Results<Ok<ProductResponseDto>, NotFound>> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        return TypedResults.Ok(product);
    }

    [HttpGet("sku/{sku}")]
    public async Task<Results<Ok<ProductResponseDto>, NotFound>> GetBySku(string sku)
    {
        var product = await _service.GetBySkuAsync(sku);
        return TypedResults.Ok(product);
    }

    [HttpGet("category/{categoryId:guid}")]
    public async Task<Ok<IEnumerable<ProductResponseDto>>> GetByCategory(Guid categoryId)
    {
        var products = await _service.GetByCategoryAsync(categoryId);
        return TypedResults.Ok(products);
    }

    [HttpPost]
    public async Task<Created<ProductResponseDto>> Create([FromBody] CreateProductDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return TypedResults.Created($"/api/product/{created.Id}", created);
    }

    [HttpPut("{id:guid}")]
    public async Task<Results<Ok<ProductResponseDto>, NotFound>> Update(Guid id, [FromBody] UpdateProductDto dto)
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