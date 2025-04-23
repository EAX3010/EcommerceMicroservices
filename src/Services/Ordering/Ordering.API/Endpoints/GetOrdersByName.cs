using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{

    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/orders/{orderName}", Handle)
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetOrdersByName");

            static async Task<IResult> Handle(string OrderName, ISender sender)
            {
                GetOrdersByNameResult result = await sender.Send(new GetOrdersByNameQuery(OrderName));
                GetOrdersByNameResponse response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            }
        }
    }
}
