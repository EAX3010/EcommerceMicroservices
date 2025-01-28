using Ordering.Domain.Interfaces;

namespace Ordering.Domain.Events
{
    public record OrderUpdatedEvent(Order order) : IDomainEvent;

}
