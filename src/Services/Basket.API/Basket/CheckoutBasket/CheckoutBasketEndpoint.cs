using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket
{
    public record BasketCheckoutRequest(BasketCheckoutDto CheckoutDto);
    public record BasketCheckoutResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPost("/checkout", Handler).Produces<BasketCheckoutResponse>()
                .ProducesProblem(StatusCodes.Status200OK)
                .WithName("CheckoutBasket");

            static async Task<IResult> Handler(BasketCheckoutRequest request, ISender sender)
            {
                var command = sender.Adapt<BasketCheckoutCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<BasketCheckoutResponse>();
                return Results.Ok(response);
            }
        }
    }
}
