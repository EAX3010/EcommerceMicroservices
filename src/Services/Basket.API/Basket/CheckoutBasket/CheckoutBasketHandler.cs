using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket
{
    public record BasketCheckoutCommand(BasketCheckoutDto CheckoutDto) : ICommand<BasketCheckoutResult>;
    public record BasketCheckoutResult(bool IsSuccess);
    public class CheckoutBasketCommandHandler : ICommandHandler<BasketCheckoutCommand, BasketCheckoutResult>
    {
        public Task<BasketCheckoutResult> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
