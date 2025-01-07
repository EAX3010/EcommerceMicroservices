namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; init; } = default!;
        public string CardHolderName { get; init; } = default!;
        public string CardExpiration { get; init; } = default!;
        public string CardSecurityNumber { get; init; } = default!;
        public int PaymentMethod { get; init; } = default!;
    }
}