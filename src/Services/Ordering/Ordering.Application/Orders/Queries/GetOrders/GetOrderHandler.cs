﻿namespace Ordering.Application.Orders.Queries.GetOrders
{
    internal class GetOrderHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderQuery, GetOrderResult>
    {

        public async Task<GetOrderResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            int pageSize = request.Page.PageSize;
            int pageIndex = request.Page.PageIndex;
            long totalItems = await dbContext.Orders.LongCountAsync(cancellationToken);
            List<Order> orders = await dbContext.Orders.Include(x => x.OrderItems)
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
