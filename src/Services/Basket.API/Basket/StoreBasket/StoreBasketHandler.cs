namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            _ = RuleFor(x => x.Cart)
                .NotNull()
                .WithMessage("Shopping cart cannot be null")
                .DependentRules(() =>
                {
                    _ = RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required");
                });
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart Cart = await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(Cart.Username);
        }
    }
}