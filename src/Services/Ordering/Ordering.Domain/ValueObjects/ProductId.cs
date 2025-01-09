using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct ProductId
    {
        private Guid Value { get; init; }
        private ProductId(Guid value) => Value = value;
        public static ProductId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("ProductId cannot be empty.");
            }

            return new ProductId(value);
        }
    }
}