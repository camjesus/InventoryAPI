namespace InventoryAPI.Domain.DTOs.Category;

public record UpdateCategoryDto(
    string Name,
    string? Description
);