namespace InventoryAPI.Domain.DTOs.Product;

public record CreateProductDto(
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    int Stock,
    Guid CategoryId
);