#region

using Ordering.Domain.Exceptions;

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderName
    {
        private const int DefaultLength = 8;

        private OrderName(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static OrderName Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Order name cannot be null, empty, or whitespace.");

            if (value.Length < DefaultLength)
                throw new DomainException($"Order name must be {DefaultLength} characters long.");

            return new OrderName(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}