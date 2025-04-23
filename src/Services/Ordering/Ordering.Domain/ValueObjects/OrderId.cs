#region

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderId
    {
        private OrderId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public static OrderId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                return new OrderId(Guid.NewGuid());
            }

            return new OrderId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}