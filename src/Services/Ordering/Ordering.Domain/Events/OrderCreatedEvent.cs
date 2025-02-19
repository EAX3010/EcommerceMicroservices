﻿using Ordering.Domain.Interfaces;

namespace Ordering.Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;