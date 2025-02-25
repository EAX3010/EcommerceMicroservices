namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; set; }
        public OrderName OrderName { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get { return OrderItems.Sum(x => x.Price * x.Quantity); }
            set => TotalPrice = 0;
        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress,
            Address billingAddress, Payment payment)
        {
            Order order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment,
            OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            OrderItem orderItem = OrderItem.Create(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            OrderItem? orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
            {
                _ = _orderItems.Remove(orderItem);
            }
        }
    }
}