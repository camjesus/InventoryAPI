using InventoryAPI.Domain.DTOs.Product;
using InventoryAPI.Domain.Exceptions;
using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Domain.Interfaces.Services;
using InventoryAPI.Domain.Mappings;
using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryService _categoryService;

    public ProductService(IProductRepository repository, ICategoryService categoryService)
    {
        _repository = repository;
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(p => p.ToResponse());
    }

    public async Task<ProductResponseDto> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);
        return product.ToResponse();
    }

    public async Task<ProductResponseDto> GetBySkuAsync(string sku)
    {
        var product = await _repository.GetBySkuAsync(sku) 
                      ?? throw new NotFoundException(nameof(Product), nameof(sku), sku);
        return product.ToResponse();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByCategoryAsync(Guid categoryId)
    {
        var products = await _repository.GetByCategoryAsync(categoryId);
        return products.Select(p => p.ToResponse());
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        await _categoryService.GetByIdAsync(dto.CategoryId);
        
        if (await _repository.ExistsBySkuAsync(dto.SKU))
            throw new DuplicateSkuException(dto.SKU);

        if (dto.Price < 0)
            throw new InvalidPriceException();

        if (dto.Stock < 0)
            throw new InvalidStockException();

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Sku = dto.SKU,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId
        };

        var created = await _repository.GetOrCreateAsync(product);
        return created.ToResponse();
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        await _categoryService.GetByIdAsync(dto.CategoryId);
        
        var product = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Product), id);

        if (dto.Price < 0)
            throw new InvalidPriceException();

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        var updated = await _repository.UpdateAsync(product);
        return updated.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Product), id);

        await _repository.DeleteAsync(id);
    }

}