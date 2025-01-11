using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct OrderName
    {
        private const int DefaultLength = 8;
        public string Value { get; init; }
        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Order name cannot be null, empty, or whitespace.");
            }

            if (value.Length != DefaultLength)
            {
                throw new DomainException($"Order name must be {DefaultLength} characters long.");
            }

            return new OrderName(value);
        }

        public override string ToString() => Value;
    }

   
}