namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        public static OrderItem Create(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            return new OrderItem
            {
                Id = OrderItemId.Of(Guid.NewGuid()),
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price,
            };
        }
        public OrderId OrderId { get; set; } = default!;//navication property
        public ProductId ProductId { get; set; } = default!;//navication property
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}