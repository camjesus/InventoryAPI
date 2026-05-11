using InventoryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAPI.Infrastructure.Data.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.HasKey(nameof(StockMovement.Id));
        
        builder.Property(sm => sm.Quantity).IsRequired();
        builder.Property(sm => sm.Reason).HasMaxLength(300);
        builder.Property(sm => sm.MovedAt).IsRequired();
        builder.Property(sm => sm.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

        builder.HasQueryFilter(sm => !sm.Product.IsDeleted);
    }
}