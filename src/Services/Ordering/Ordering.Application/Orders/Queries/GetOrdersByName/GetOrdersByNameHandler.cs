namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrderByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request,
            CancellationToken cancellationToken)
        {
            List<Order> orders = await dbContext.Orders.Where(o => o.OrderName.Value == OrderName.Of(request.Name).Value)
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderName.Value).ToListAsync(cancellationToken).ConfigureAwait(false);

            IEnumerable<OrderDto> orderDtos = orders.Select(p => p.ToDto());
            return new GetOrdersByNameResult(orderDtos);
        }
    }
}