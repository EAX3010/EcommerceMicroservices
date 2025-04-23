namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest Page) : IQuery<GetOrderResult>;
    public record GetOrderResult(PaginationResult<OrderDto>? Data);

}
