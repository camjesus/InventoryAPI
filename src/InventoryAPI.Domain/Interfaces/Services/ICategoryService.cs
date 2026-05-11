using InventoryAPI.Domain.DTOs.Category;

namespace InventoryAPI.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> GetByIdAsync(Guid id);
    Task<CategoryResponseDto> UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task DeleteAsync(Guid id);
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
}