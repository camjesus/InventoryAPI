namespace InventoryAPI.Domain.DTOs.Category;

public record CreateCategoryDto(
    string Name,
    string? Description
);