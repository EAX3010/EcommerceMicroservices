namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; set; } //navication property
    public ProductId ProductId { get; set; } //navication property
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public static OrderItem Create(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        return new OrderItem
        {
            Id = OrderItemId.Of(Guid.NewGuid()),
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Price = price
        };
    }
}