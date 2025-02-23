namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrderByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request,
            CancellationToken cancellationToken)
        {
            List<Order> orders = await dbContext.Orders.Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(request.Name))
                .OrderBy(o => o.OrderName).ToListAsync(cancellationToken).ConfigureAwait(false);

            IEnumerable<OrderDto> orderDtos = orders.Select(p => p.ToDto());
            return new GetOrdersByNameResult(orderDtos);
        }
    }
}