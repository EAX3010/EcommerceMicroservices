using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto OrderDto);
    public record CreateOrderResponse(Guid OrderId);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPost("/orders", Handle)
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("CreateOrder");

            static async Task<IResult> Handle(CreateOrderRequest request, ISender sender)
            {
                CreateOrderCommand command = request.Adapt<CreateOrderCommand>();

                CreateOrderResult result = await sender.Send(command);

                CreateOrderResponse response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.OrderId}", response);
            }
        }
    }
}
