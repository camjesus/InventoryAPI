using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    Task<Product?> GetBySkuAsync(string sku);
    Task<bool> ExistsBySkuAsync(string sku);
    Task<Product> GetOrCreateAsync(Product product);
}