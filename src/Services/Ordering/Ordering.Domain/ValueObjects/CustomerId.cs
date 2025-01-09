using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct CustomerId
    {
        private Guid Value { get; init; }
        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty.");
            }

            return new CustomerId(value);
        }
        public override string ToString() => Value.ToString();
    }
}