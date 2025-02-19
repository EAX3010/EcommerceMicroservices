namespace Ordering.Application.Mappers
{
    public static class Mapper
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                order.ShippingAddress.ToDto(),
                order.BillingAddress.ToDto(),
                order.Payment.ToDto(),
                order.Status,
                order.OrderItems.Select(item => new OrderItemDto(
                    item.Id.Value,
                    item.ProductId.Value,
                    item.Quantity,
                    item.Price
                )).ToList()
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
            var order = Order.Create(
                OrderId.Of(dto.Id),
                CustomerId.Of(dto.CustomerId),
                OrderName.Of(dto.OrderName),
                dto.ShippingAddress.ToAddress(),
                dto.BillingAddress.ToAddress(),
                dto.Payment.ToPayment());

            foreach (var item in dto.OrderItems) order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
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
    }
}