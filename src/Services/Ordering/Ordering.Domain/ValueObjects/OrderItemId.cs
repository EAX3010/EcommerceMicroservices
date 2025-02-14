using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderItemId
    {
        public Guid Value { get; init; }
        private OrderItemId(Guid value) => Value = value;

        public static OrderItemId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }

            return new OrderItemId(value);
        }
        public override string ToString() => Value.ToString();
    }


}