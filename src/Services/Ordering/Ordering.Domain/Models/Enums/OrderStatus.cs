namespace Ordering.Domain.Models.Enums
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