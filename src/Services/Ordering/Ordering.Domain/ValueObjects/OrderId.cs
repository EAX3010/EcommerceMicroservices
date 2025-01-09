using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderId
    {
        private Guid Value { get; init; }
        private OrderId(Guid value) => Value = value;

        public static OrderId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Order ID cannot be empty.");
            }
            return new OrderId(value);
        }

        public override string ToString() => Value.ToString();
    }

   
}