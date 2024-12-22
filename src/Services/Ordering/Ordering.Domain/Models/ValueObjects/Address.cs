namespace Ordering.Domain.Models.ValueObjects
{
    public record CustomerId
    {
        public Guid value { get; init; } = default!;
    }
}