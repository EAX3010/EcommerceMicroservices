#region

using Ordering.Domain.Exceptions;

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct ProductId
    {
        private ProductId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public static ProductId Of(Guid value)
        {
            if (value == Guid.Empty) throw new DomainException("ProductId cannot be empty.");

            return new ProductId(value);
        }
    }
}