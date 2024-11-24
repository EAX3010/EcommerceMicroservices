namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPost("/basket", Handle).Produces<StoreBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("StoreBasket");

            static async Task<IResult> Handle(StoreBasketRequest request, ISender sender)
            {
                StoreBasketCommand command = request.Adapt<StoreBasketCommand>();

                StoreBasketResult result = await sender.Send(command);

                StoreBasketResponse response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.Username}", response);
            }
        }
    }
}