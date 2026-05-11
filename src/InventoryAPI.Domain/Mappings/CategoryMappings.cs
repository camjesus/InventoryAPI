using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Mappings;

public static class CategoryMappings
{
    public static CategoryResponseDto ToResponse(this Category category) => new(
        category.Id,
        category.Name,
        category.Description,
        category.CreatedAt,
        category.UpdatedAt
    );
}