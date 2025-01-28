using MediatR;

namespace Ordering.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.Now;
        string EventType => GetType().AssemblyQualifiedName!;

    }
}
