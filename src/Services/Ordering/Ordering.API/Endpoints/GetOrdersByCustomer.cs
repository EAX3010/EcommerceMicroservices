using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{

    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/orders/customer/{customerId}", Handle)
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetOrdersByCustomer");

            static async Task<IResult> Handle(Guid CustomerId, ISender sender)
            {
                GetOrdersByCustomerResult result = await sender.Send(new GetOrdersByCustomerQuery(CustomerId));
                GetOrdersByCustomerResponse response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            }
        }
    }
}
