namespace Ordering.Application.Orders.Queries.GetOrders
{
    internal class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrderResult>
    {

        public async Task<GetOrderResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            int pageSize = request.Page.PageSize;
            int pageIndex = request.Page.PageIndex;
            long totalItems = await dbContext.Orders.LongCountAsync(cancellationToken);
            List<Order> orders = await dbContext.Orders.Include(x => x.OrderItems).AsNoTracking()
                .OrderBy(o => o.OrderName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return new GetOrderResult(new
                PaginationResult<OrderDto>(pageIndex,
                pageSize,
                totalItems,
                orders.Select(o => o.ToDto())));
        }
    }
}
