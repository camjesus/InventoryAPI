namespace InventoryAPI.Domain.DTOs.Product;

public record UpdateProductDto(
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId
);