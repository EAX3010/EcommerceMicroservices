namespace Ordering.Application.Orders.Queries.GetOrders
{
    internal class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrderResult>
    {

        public async Task<GetOrderResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            int pageSize = request.Page.PageSize;
            int pageIndex = request.Page.PageIndex;
            int totalItems = await dbContext.Orders.CountAsync();

            List<Order> orders = await dbContext.Orders.Include(x => x.OrderItems).AsNoTracking()
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrderResult(new
                PaginationResult<OrderDto>(pageIndex,
                pageSize,
                totalItems,
                orders.Select(o => o.ToDto())));
        }
    }
}
