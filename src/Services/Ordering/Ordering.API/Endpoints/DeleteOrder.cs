using Ordering.Application.Orders.Commands.DeleteOrder;
namespace Ordering.API.Endpoints
{
    public class DeleteOrder
    {
        public record DeleteOrderResponse(bool IsSuccess);
        public class UpdateOrder : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                _ = app.MapDelete("/orders{id}", Handle)
                    .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                    .ProducesProblem(StatusCodes.Status400BadRequest)
                    .ProducesProblem(StatusCodes.Status404NotFound)
                    .WithName("DeleteOrder");

                static async Task<IResult> Handle(Guid id, ISender sender)
                {
                    DeleteOrderResult result = await sender.Send(new DeleteOrderCommand(id));
                    DeleteOrderResponse response = result.Adapt<DeleteOrderResponse>();
                    return Results.Ok(response);
                }
            }
        }
    }
}
