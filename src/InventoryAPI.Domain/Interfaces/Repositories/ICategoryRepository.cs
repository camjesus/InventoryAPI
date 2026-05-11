using InventoryAPI.Entities;

namespace InventoryAPI.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Category> GetOrCreateAsync(Category category);
}