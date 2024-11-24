namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    //public record StoreBasketResult(bool IsSuccess = false);
    public record StoreBasketResult(string Username);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            _ = RuleFor(x => x.ShoppingCart).NotNull().WithMessage("ShoppingCart is required");
            _ = RuleFor(x => x.ShoppingCart.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreBasketCommandHandler(IDocumentSession session) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart shoppingCart = request.ShoppingCart;


            return new StoreBasketResult("eax");
        }
    }
}