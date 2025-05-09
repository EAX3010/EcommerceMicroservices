using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket
{
    public record BasketCheckoutRequest(BasketCheckoutDto CheckoutDto);
    public record BasketCheckoutResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPost("/checkout", async (BasketCheckoutRequest request, ISender sender) =>
            {
                var result = await sender.Send(new BasketCheckoutCommand(request.CheckoutDto));
                var response = result.Adapt<BasketCheckoutResponse>();
                return Results.Ok(response);

            }).Produces<BasketCheckoutResponse>()
                .ProducesProblem(StatusCodes.Status200OK)
                .WithName("CheckoutBasket");


        }
    }
}
