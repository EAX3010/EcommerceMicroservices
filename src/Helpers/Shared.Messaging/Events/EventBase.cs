namespace Shared.Messaging.Events
{
    public record EventBase
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName!;

    }
}
