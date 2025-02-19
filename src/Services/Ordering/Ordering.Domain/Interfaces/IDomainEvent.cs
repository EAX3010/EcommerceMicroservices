using MediatR;

namespace Ordering.Domain.Interfaces;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTime OccurredOn => DateTime.UtcNow;
    string EventType => GetType().AssemblyQualifiedName!;
}