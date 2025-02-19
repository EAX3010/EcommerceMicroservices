#region

using Ordering.Application.Exceptions;

#endregion

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = command.OrderId;
            var dbOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (dbOrder == null) throw new OrderNotFoundException(orderId);
            dbContext.Orders.Remove(dbOrder);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}