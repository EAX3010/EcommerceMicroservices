using Basket.API.Dtos;
using MassTransit;
using Shared.Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket
{
    public record BasketCheckoutCommand(BasketCheckoutDto CheckoutDto) : ICommand<BasketCheckoutResult>;
    public record BasketCheckoutResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator : AbstractValidator<BasketCheckoutCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            _ = RuleFor(x => x.CheckoutDto).NotNull().WithMessage("CheckoutDto cannot be null");
            _ = RuleFor(x => x.CheckoutDto.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<BasketCheckoutCommand, BasketCheckoutResult>
    {
        public async Task<BasketCheckoutResult> Handle(BasketCheckoutCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart basekt = await repository.GetBasket(command.CheckoutDto.UserName, cancellationToken);
            if (basekt == null)
            {
                return new BasketCheckoutResult(false);
            }
            BasketCheckoutEvent eventMessage = command.CheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basekt.TotalPrice;
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            _ = await repository.DeleteBasket(command.CheckoutDto.UserName, cancellationToken);
            return new BasketCheckoutResult(true);
        }
    }
}
