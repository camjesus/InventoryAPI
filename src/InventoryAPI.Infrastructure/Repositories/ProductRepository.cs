using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Entities;
using InventoryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet.Where(p => p.Sku == sku)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Sku.ToUpper() == sku.ToUpper());
    }

    public async Task<bool> ExistsBySkuAsync(string sku)
    {
        return await _dbSet.AnyAsync(p => p.Sku == sku);
    }

    public async Task<Product> GetOrCreateAsync(Product product)
    {
        var existing = await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Sku.ToUpper() == product.Sku.ToUpper());

        if (existing is not null)
            return existing;

        await _dbSet.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .ToListAsync();
    }
}