using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Entities;
using InventoryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<Category> GetOrCreateAsync(Category category)
    {
        var existing = await _dbSet
            .FirstOrDefaultAsync(c => c.Name.Equals(category.Name, StringComparison.CurrentCultureIgnoreCase));

        if (existing is not null)
            return existing;

        await _dbSet.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }
}