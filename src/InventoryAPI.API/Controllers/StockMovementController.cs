using InventoryAPI.Domain.DTOs.StockMovement;
using InventoryAPI.Domain.Interfaces.Services;
using InventoryAPI.Entities.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.API.Controllers;

[ApiController]
[Route("api/stockmovement")]
public class StockMovementController : ControllerBase
{
    private readonly IStockMovementService _service;

    public StockMovementController(IStockMovementService service)
    {
        _service = service;
    }

    [HttpGet("product/{productId:guid}")]
    public async Task<Results<Ok<IEnumerable<StockMovementResponseDto>>, NotFound>> GetByProduct(Guid productId)
    {
        var movements = await _service.GetByProductAsync(productId);
        return TypedResults.Ok(movements);
    }

    [HttpGet("type/{type}")]
    public async Task<Ok<IEnumerable<StockMovementResponseDto>>> GetByType(MovementType type)
    {
        var movements = await _service.GetByTypeAsync(type);
        return TypedResults.Ok(movements);
    }

    [HttpPost]
    public async Task<Results<Created<StockMovementResponseDto>, NotFound, BadRequest>> Create([FromBody] CreateStockMovementDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return TypedResults.Created($"/api/stockmovement/{created.Id}", created);
    }
}