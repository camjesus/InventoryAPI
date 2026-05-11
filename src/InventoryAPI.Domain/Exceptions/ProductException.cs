namespace InventoryAPI.Domain.Exceptions;

    public class DuplicateSkuException : DomainException
    {
        public DuplicateSkuException(string sku)
            : base("DUPLICATE_SKU", $"A product with SKU '{sku}' already exists.") { }
    }

    public class InvalidStockException : DomainException
    {
        public InvalidStockException()
            : base("INVALID_STOCK", "Stock cannot be negative.") { }
    }
    
    public class InvalidPriceException : DomainException
    {
        public InvalidPriceException()
            : base("INVALID_PRICE", "Price cannot be negative.") { }
    }