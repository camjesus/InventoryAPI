namespace InventoryAPI.Domain.Exceptions;

public class InvalidQuantityException : DomainException
{
    public InvalidQuantityException()
        : base("INVALID_QUANTITY", "Quantity cannot be zero.") { }
}


public class InvalidMovementException : DomainException
{
    public InvalidMovementException()
        : base("INVALID_MOVEMENT_TYPE", "Movement type is not valid.") { }
}

public class InsuficientStockException : DomainException
{
    public InsuficientStockException(string name, int stock)
        : base("INSUFFICIENT_STOCK", $"Product '{name}' only has {stock} units available.") { }
}

