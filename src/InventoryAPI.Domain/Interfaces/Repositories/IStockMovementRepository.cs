using InventoryAPI.Entities;
using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Domain.Interfaces.Repositories;

public interface IStockMovementRepository
{
    Task<IEnumerable<StockMovement>> GetByProductAsync(Guid productId);
    Task<IEnumerable<StockMovement>> GetByTypeAsync(MovementType type);
    Task<StockMovement> CreateAsync(StockMovement movement);
}