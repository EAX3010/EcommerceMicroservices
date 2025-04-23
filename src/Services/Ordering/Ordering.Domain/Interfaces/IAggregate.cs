namespace Ordering.Domain.Interfaces
{
    public interface IAggregate
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}