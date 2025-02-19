#region

using Ordering.Domain.Interfaces;

#endregion

namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvent;
}