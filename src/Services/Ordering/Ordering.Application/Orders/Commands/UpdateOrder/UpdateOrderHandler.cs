using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = command.orderDto.ToOrder();
            var orderId = order.Id;

            var DbOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (DbOrder == null)
            {
                throw new OrderNotFoundException(orderId.Value);
            }
            DbOrder.Update(order.OrderName, order.ShippingAddress, order.BillingAddress, order.Payment, order.Status);
            dbContext.Orders.Update(DbOrder);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }

    }
}
