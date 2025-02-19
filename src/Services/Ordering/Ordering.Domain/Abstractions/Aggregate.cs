﻿#region

using Ordering.Domain.Interfaces;

#endregion

namespace Ordering.Domain.Abstractions
{
    public class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();

        public IDomainEvent[] ClearDomainEvents()
        {
            var domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}