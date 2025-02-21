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
            Guid orderId = command.OrderId;
            Order? dbOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken)
                ?? throw new OrderNotFoundException(orderId);
            _ = dbContext.Orders.Remove(dbOrder);
            _ = await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}