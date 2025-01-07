﻿using Ordering.Domain.Exceptions;
using Ordering.Domain.Models;

namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; private set; }

        private OrderItemId(Guid value) => Value = value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }

            return new OrderItemId(value);
        }
    }
}