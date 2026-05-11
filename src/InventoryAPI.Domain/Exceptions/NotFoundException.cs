namespace InventoryAPI.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} with id '{id}' was not found.") { }

    public NotFoundException(string entity, string field, string value)
        : base($"{entity} with {field} '{value}' was not found.") { }
}