using Shared.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id)
        : base($"Order with Id {id} was not found")
    {
    }

    public string? EntityName { get; }
    public object? EntityId { get; }
}