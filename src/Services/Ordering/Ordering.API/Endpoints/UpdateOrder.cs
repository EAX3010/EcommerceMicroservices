using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
  {
    public record UpdateOrderRequest(OrderDto OrderDto);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPut("/orders", Handle)
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("UpdateOrder");

            static async Task<IResult> Handle(UpdateOrderRequest request, ISender sender)
            {
                UpdateOrderCommand command = request.Adapt<UpdateOrderCommand>();
                UpdateOrderResult result = await sender.Send(command);
                UpdateOrderResponse response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            }
        }
    }
}
