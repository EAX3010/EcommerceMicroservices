namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string Username);

public record DeleteBasketResponse(bool IsSuccess = false);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket", Handle).Produces<DeleteBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("DeleteBasket");

        async Task<IResult> Handle(DeleteBasketRequest request, ISender sender)
        {
            var commmand = request.Adapt<DeleteBasketCommand>();
            var result = sender.Send(commmand);
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        }
    }
}