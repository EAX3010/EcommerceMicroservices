namespace Shared.Messaging.Events
{
    public record IntegrationEvent
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName!;

    }
}
