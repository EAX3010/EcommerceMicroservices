using Ordering.Application.Orders.Commands.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Application.Mappers
{
    public static class Mapper
    {
        public static OrderDto ToDto(this Order order)
        {
            List<OrderItemDto> orderItems = [.. order.OrderItems.Select(item => new OrderItemDto(
                    item.Id.Value,
                    item.ProductId.Value,
                    item.Quantity,
                    item.Price
                ))];
            return new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                order.ShippingAddress.ToDto(),
                order.BillingAddress.ToDto(),
                order.Payment.ToDto(),
                order.Status,
                orderItems
            );
        }

        public static AddressDto ToDto(this Address address)
        {
            return new AddressDto(
                address.FirstName,
                address.LastName,
                address.EmailAddress,
                address.AddressLine,
                address.State,
                address.Country,
                address.ZipCode
            );
        }

        public static PaymentDto ToDto(this Payment payment)
        {
            return new PaymentDto(
                payment.CardNumber,
                payment.CardHolderName,
                payment.CardExpiration,
                payment.CardSecurityNumber,
                payment.PaymentMethod
            );
        }

        public static Order ToOrder(this OrderDto dto)
        {
            Order order = Order.Create(
                OrderId.Of(dto.Id),
                CustomerId.Of(dto.CustomerId),
                OrderName.Of(dto.OrderName),
                dto.ShippingAddress.ToAddress(),
                dto.BillingAddress.ToAddress(),
                dto.Payment.ToPayment());

            foreach (OrderItemDto item in dto.OrderItems)
            {
                order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            return order;
        }

        public static Address ToAddress(this AddressDto dto)
        {
            return Address.Of(
                dto.FirstName,
                dto.LastName,
                dto.EmailAddress,
                dto.AddressLine,
                dto.State,
                dto.Country,
                dto.ZipCode);
        }

        public static Payment ToPayment(this PaymentDto dto)
        {
            return Payment.Of(
                dto.CardNumber,
                dto.CardHolderName,
                dto.CardExpiration,
                dto.CardSecurityNumber,
                1);
        }
        public static CreateOrderCommand ToCreateOrderCommand(this BasketCheckoutEvent basketCheckoutEvent)
        {
            AddressDto addressDto = new(basketCheckoutEvent.FirstName,
                basketCheckoutEvent.LastName,
                basketCheckoutEvent.EmailAddress,
                basketCheckoutEvent.AddressLine,
                basketCheckoutEvent.State,
                basketCheckoutEvent.Country,
                basketCheckoutEvent.ZipCode);

            PaymentDto paymentDto = new(basketCheckoutEvent.CardNumber,
                basketCheckoutEvent.CardName,
                basketCheckoutEvent.Expiration,
                basketCheckoutEvent.CVV,
                basketCheckoutEvent.PaymentMethod);

            Guid orderId = Guid.NewGuid();

            OrderDto orderDto = new(
                Id: orderId,
                CustomerId: basketCheckoutEvent.CustomerId,
                OrderName: basketCheckoutEvent.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems:
                [
                   new OrderItemDto(
                        Id: orderId,
                        ProductId: new Guid("528EAF31-3343-4875-A7EA-2CAE85084590"),
                        Quantity: 1,
                        Price: 1151
                    ),
                    new OrderItemDto(
                        Id: orderId,
                        ProductId: new Guid("9FF66B07-A7B1-4E66-A736-56310C63E663"),
                        Quantity: 1,
                        Price: 1930
                    )
                ]);

            return new CreateOrderCommand(orderDto);

        }
    }
}