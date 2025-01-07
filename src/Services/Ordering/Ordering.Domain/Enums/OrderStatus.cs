namespace Ordering.Domain.Enums
{
    public enum OrderStatus : uint
    {
        Draft,
        Pending,
        Paid,
        Shipped,
        Canceled,
        Completed,
    }

}