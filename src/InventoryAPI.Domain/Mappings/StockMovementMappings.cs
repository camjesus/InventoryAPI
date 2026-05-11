using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Domain.DTOs.StockMovement;
using InventoryAPI.Entities;
namespace InventoryAPI.Domain.Mappings;

public static class StockMovementMappings
{
    public static StockMovementResponseDto ToResponse(this StockMovement movement) => new(
        movement.Id,
        movement.ProductId,
        movement.Product?.Name ?? string.Empty,
        movement.Product?.Sku ?? string.Empty,
        movement.Quantity,
        movement.Type,
        movement.Reason,
        movement.MovedAt
    );
}