namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpointHandler : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/basket/{userName}", Handle).Produces<GetBasketResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetBasket");

            static async Task<IResult> Handle(string userName, ISender sender)
            {
                GetBasketResult result = await sender.Send(new GetBasketQuery(userName));

                GetBasketResponse response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            }
        }
    }
}