#region

using Ordering.Domain.Exceptions;

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderItemId
    {
        private OrderItemId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public static OrderItemId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }

            return new OrderItemId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}