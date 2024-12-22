﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvent[] ClearDomainEvents()
        {
            var domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }
    }
}