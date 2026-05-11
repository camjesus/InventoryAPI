namespace InventoryAPI.Domain.DTOs.Product;

public record ProductResponseDto(
    Guid Id,
    string Name,
    string? Description,
    string Sku,
    decimal Price,
    int Stock,
    Guid CategoryId,
    string CategoryName,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);