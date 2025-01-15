using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = "Seeder";
            LastModified = DateTime.UtcNow;
            LastModifiedBy = "Seeder";
        }
        public OrderId OrderId { get; set; } = default!;//navication property
        public ProductId ProductId { get; set; } = default!;//navication property
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}