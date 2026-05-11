using InventoryAPI.Domain.DTOs.Product;

namespace InventoryAPI.Domain.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto> GetByIdAsync(Guid id);
    Task<ProductResponseDto> GetBySkuAsync(string sku);
    Task<IEnumerable<ProductResponseDto>> GetByCategoryAsync(Guid categoryId);
    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
    Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto);
    Task DeleteAsync(Guid id);
}