#region

using Ordering.Domain.Exceptions;

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct CustomerId
    {
        private CustomerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public static CustomerId Of(Guid value)
        {
            if (value == Guid.Empty) throw new DomainException("CustomerId cannot be empty.");

            return new CustomerId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}