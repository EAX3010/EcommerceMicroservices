using Shared.Exceptions;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public string? EntityName { get; }
        public object? EntityId { get; }

        public OrderNotFoundException(Guid id)
         : base($"Order with Id {id} was not found")
        {
        }
    }
}