using InventoryAPI.Domain.DTOs.StockMovement;
using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Domain.Interfaces.Services;

public interface IStockMovementService
{
    Task<IEnumerable<StockMovementResponseDto>> GetByProductAsync(Guid productId);
    Task<IEnumerable<StockMovementResponseDto>> GetByTypeAsync(MovementType type);
    Task<StockMovementResponseDto> CreateAsync(CreateStockMovementDto dto);
}