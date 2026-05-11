using InventoryAPI.Domain.DTOs.StockMovement;
using InventoryAPI.Domain.Exceptions;
using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Domain.Interfaces.Services;
using InventoryAPI.Domain.Mappings;
using InventoryAPI.Entities;
using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Domain.Services;

public class StockMovementService : IStockMovementService
{
    private readonly IStockMovementRepository _movementRepository;
    private readonly IProductRepository _productRepository;

    public StockMovementService(
        IStockMovementRepository movementRepository,
        IProductRepository productRepository)
    {
        _movementRepository = movementRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<StockMovementResponseDto>> GetByProductAsync(Guid productId)
    {
        _ = await _productRepository.GetByIdAsync(productId)
            ?? throw new NotFoundException(nameof(Product), productId);

        var movements = await _movementRepository.GetByProductAsync(productId);
        return movements.Select(m => m.ToResponse());
    }

    public async Task<IEnumerable<StockMovementResponseDto>> GetByTypeAsync(MovementType type)
    {
        var movements = await _movementRepository.GetByTypeAsync(type);
        return movements.Select(m => m.ToResponse());
    }

    public async Task<StockMovementResponseDto> CreateAsync(CreateStockMovementDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId)
            ?? throw new NotFoundException(nameof(Product), dto.ProductId);

        if (dto.Quantity == 0)
            throw new InvalidQuantityException();

        var newStock = dto.Type switch
        {
            MovementType.Purchase or MovementType.Return => product.Stock + dto.Quantity,
            MovementType.Sale or MovementType.Adjustment => product.Stock - dto.Quantity,
            _ => throw new InvalidMovementException()
        };

        if (newStock < 0)
            throw new InsuficientStockException(product.Name, product.Stock);

        product.Stock = newStock;
        await _productRepository.UpdateAsync(product);

        var movement = new StockMovement
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Type = dto.Type,
            Reason = dto.Reason
        };

        var created = await _movementRepository.CreateAsync(movement);
        return created.ToResponse();
    }
}