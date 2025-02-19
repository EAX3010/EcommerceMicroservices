using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public readonly record struct OrderId
{
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static OrderId Of(Guid value)
    {
        if (value == Guid.Empty) throw new DomainException("Order ID cannot be empty.");
        return new OrderId(value);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}