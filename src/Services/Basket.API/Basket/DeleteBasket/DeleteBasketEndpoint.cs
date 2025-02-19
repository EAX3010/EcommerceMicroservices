namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSuccess = false);

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapDelete("/basket/{userName}", Handle).Produces<DeleteBasketResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("DeleteBasket");

            static async Task<IResult> Handle(string userName, ISender sender)
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            }
        }
    }
}