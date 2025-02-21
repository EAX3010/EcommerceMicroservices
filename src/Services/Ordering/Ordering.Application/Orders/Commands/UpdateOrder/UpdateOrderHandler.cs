#region

using Ordering.Application.Exceptions;

#endregion

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Order order = command.OrderDto.ToOrder();
            OrderId orderId = order.Id;

            Order? DbOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken) ??
                throw new OrderNotFoundException(orderId.Value);
            DbOrder.Update(order.OrderName, order.ShippingAddress, order.BillingAddress, order.Payment, order.Status);
            _ = dbContext.Orders.Update(DbOrder);
            _ = await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }
    }
}