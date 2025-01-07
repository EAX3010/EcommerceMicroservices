using Ordering.Domain.Exceptions;
using Ordering.Domain.Models;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 8;
        public string Value { get; private set; }

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            return new OrderName(value);
        }
    }
}