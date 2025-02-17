using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = command.OrderId;
            var DbOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (DbOrder == null)
            {
                throw new OrderNotFoundException(orderId);
            }
            dbContext.Orders.Remove(DbOrder);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }

    }
}