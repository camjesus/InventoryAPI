using InventoryAPI.Domain.DTOs.Product;
using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Mappings;

public static class ProductMappings
{
    public static ProductResponseDto ToResponse(this Product product) => new(
        product.Id,
        product.Name,
        product.Description,
        product.Sku,
        product.Price,
        product.Stock,
        product.CategoryId,
        product.Category?.Name ?? string.Empty,
        product.CreatedAt,
        product.UpdatedAt
    );
}