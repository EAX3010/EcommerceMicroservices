namespace Shared.Messaging.Events
{
    public record EventBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;

    }
}
