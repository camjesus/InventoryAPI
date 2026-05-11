using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Domain.DTOs.StockMovement;

public record StockMovementResponseDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    int Quantity,
    MovementType Type,
    string? Reason,
    DateTime MovedAt
);