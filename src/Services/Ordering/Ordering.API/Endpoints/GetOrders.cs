
using Ordering.Application.Orders.Queries.GetOrders;
using Shared.Pagination;

namespace Ordering.API.Endpoints
{
    public record GetOrderResponse(PaginationResult<OrderDto> result);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/orders", Handle)
                .Produces<GetOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetOrders");
            static async Task<IResult> Handle([AsParameters] PaginationRequest request, ISender sender)
            {
                GetOrderResult result = await sender.Send(new GetOrdersQuery(request));
                GetOrderResponse response = result.Adapt<GetOrderResponse>();
                return Results.Ok(response);
            }

        }
    }
}
