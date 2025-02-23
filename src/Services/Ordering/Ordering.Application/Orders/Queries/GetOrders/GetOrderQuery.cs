namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrderQuery(PaginationRequest Page) : IQuery<GetOrderResult>;
    public record GetOrderResult(PaginationResult<OrderDto> result);

}
