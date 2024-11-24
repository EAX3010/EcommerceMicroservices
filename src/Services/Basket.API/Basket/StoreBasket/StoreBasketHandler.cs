namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

//public record StoreBasketResult(bool IsSuccess = false);
public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("ShoppingCart is required");
        RuleFor(x => x.ShoppingCart.Username).NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = request.ShoppingCart;
       

        return new StoreBasketResult("eax");
    }
}