using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Domain.DTOs.StockMovement;

public record CreateStockMovementDto(
    Guid ProductId,
    int Quantity,
    MovementType Type,
    string? Reason
);