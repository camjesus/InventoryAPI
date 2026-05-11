namespace InventoryAPI.Domain.DTOs.Category;

public record CategoryResponseDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);