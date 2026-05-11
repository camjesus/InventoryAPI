using InventoryAPI.Entities.Enums;

namespace InventoryAPI.Entities;

public class StockMovement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string? Reason { get; set; }
    public DateTime MovedAt { get; set; } = DateTime.UtcNow;
}