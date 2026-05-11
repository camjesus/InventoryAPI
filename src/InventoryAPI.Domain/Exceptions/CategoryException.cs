namespace InventoryAPI.Domain.Exceptions;

public class DuplicateNameException : DomainException
{
    public DuplicateNameException(string name)
        : base("DUPLICATE_NAME", $"A category with name '{name}' already exists.") { }
}