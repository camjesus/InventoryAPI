using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Domain.Exceptions;
using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Domain.Interfaces.Services;
using InventoryAPI.Domain.Mappings;
using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => c.ToResponse());
    }

    public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Category), id);
        return category.ToResponse();
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new DuplicateNameException(dto.Name);

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await _repository.GetOrCreateAsync(category);
        return created.ToResponse();
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Category), id);

        category.Name = dto.Name;
        category.Description = dto.Description;

        var updated = await _repository.UpdateAsync(category);
        return updated.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Category), id);

        await _repository.DeleteAsync(id);
    }

    public async Task<CategoryResponseDto> GetOrCreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var result = await _repository.GetOrCreateAsync(category);
        return result.ToResponse();
    }
}