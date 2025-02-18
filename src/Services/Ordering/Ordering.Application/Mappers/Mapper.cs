
namespace Ordering.Application.Mappers
{
    public static class Mapper
    {
        public static Order ToOrder(this OrderDto dto)
        {
            var order = Order.Create(
                OrderId.Of(dto.Id),
                CustomerId.Of(dto.CustomerId),
                OrderName.Of(dto.OrderName),
                dto.ShippingAddress.ToAddress(),
                dto.BillingAddress.ToAddress(),
                dto.Payment.ToPayment());

            foreach (var item in dto.OrderItems)
            {
                order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }
            return order;
        }
        public static Address ToAddress(this AddressDto dto) => Address.Of(
                dto.FirstName,
                dto.LastName,
                dto.EmailAddress,
                dto.AddressLine,
                dto.State,
                dto.Country,
                dto.ZipCode);

        public static Payment ToPayment(this PaymentDto dto) =>
             Payment.Of(
                dto.CardNumber,
                dto.CardHolderName,
                dto.CardExpiration,
                dto.CardSecurityNumber,
                1);
    }
}
