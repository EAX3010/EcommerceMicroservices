namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request,
            CancellationToken cancellationToken)
        {
            List<Order> orders = await dbContext.Orders.Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(request.Customer))
                .OrderBy(o => o.OrderName).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<OrderDto> orderDtos = [.. orders.Select(p => p.ToDto())];
            return new GetOrdersByCustomerResult(orderDtos);
        }
    }
}