#region

using Ordering.Domain.Interfaces;

#endregion

namespace Ordering.Domain.Events
{
    public record OrderUpdatedEvent(Order order) : IDomainEvent;
}