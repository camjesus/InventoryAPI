using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Entities;
using InventoryAPI.Entities.Enums;
using InventoryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Infrastructure.Repositories;

public class StockMovementRepository : IStockMovementRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<StockMovement> _dbSet;

    public StockMovementRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<StockMovement>();
    }
    
    public async Task<IEnumerable<StockMovement>> GetByProductAsync(Guid productId)
    {
        return await _dbSet
            .Where(p=> p.ProductId == productId)
            .Include(p => p.Product)
            .OrderByDescending(p => p.MovedAt)
            .ToListAsync();;
    }

    public async Task<IEnumerable<StockMovement>> GetByTypeAsync(MovementType type)
    {
        return await _dbSet
            .Where(p => p.Type == type)
            .Include(p => p.Product)
            .OrderByDescending(p => p.MovedAt)
            .ToListAsync();
    }

    public async Task<StockMovement> CreateAsync(StockMovement movement)
    {
        await _dbSet.AddAsync(movement);
        await  _context.SaveChangesAsync();
        return movement;
    }
}