using AutoFixture;
using AutoFixture.AutoMoq;
using InventoryAPI.Entities;
using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Tests.Customizations;

public class InventoryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Category>(c => c
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Name, () => $"Category-{Guid.NewGuid().ToString()[..8]}")
            .With(x => x.Description, () => $"Description-{Guid.NewGuid().ToString()[..8]}")
            .With(x => x.IsDeleted, false)
            .With(x => x.CreatedAt, DateTime.UtcNow)
            .Without(x => x.Products));

        fixture.Customize<Product>(c => c
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Name, () => $"Product-{Guid.NewGuid().ToString()[..8]}")
            .With(x => x.Sku, () => $"SKU-{Guid.NewGuid().ToString()[..8]}")
            .With(x => x.Price, 99.99m)
            .With(x => x.Stock, () => new Random().Next(1, 50))
            .With(x => x.IsDeleted, false)
            .With(x => x.CreatedAt, DateTime.UtcNow)
            .Without(x => x.Category)
            .Without(x => x.StockMovements));

        fixture.Customize<StockMovement>(c => c
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Quantity, () => new Random().Next(1, 50))
            .With(x => x.Type, MovementType.Purchase)
            .With(x => x.MovedAt, DateTime.UtcNow)
            .Without(x => x.Product));
    }
}