using Shared.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string Username)
        : base($"Basket for user {Username} was not found")
    {
    }

    public BasketNotFoundException(string name, object id) : base($"Entity {name} {id} was not Found")
    {
        EntityName = name;
        EntityId = id;
    }

    public string? EntityName { get; }
    public object? EntityId { get; }
}