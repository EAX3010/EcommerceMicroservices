using Discount.gRPC;
using Grpc.Core;
using NetTopologySuite.Index.HPRtree;

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

    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient Proto, ILogger<StoreBasketCommandHandler> logger) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await GetDiscount(command.Cart, cancellationToken);
            ShoppingCart Cart = await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(Cart.Username);
        }
        public async Task GetDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            IEnumerable<Task> discountTasks = cart.Items.Select(item =>
            {
                return ApplyDiscountToItem(item, cancellationToken);
            });
            await Task.WhenAll(discountTasks);
        }
        public async Task ApplyDiscountToItem(ShoppingCartItem item, CancellationToken cancellationToken)
        {
            item.DiscountAmount = 0;
            try
            {
                CouponModel coupon = await Proto.GetDiscountAsync(
                     new GetDiscountRequest { ProductName = item.ProductName },
                     deadline: null,
                     headers: null,
                     cancellationToken: cancellationToken);
                item.DiscountAmount = coupon.Amount;

            }
            catch (RpcException e) when (e.StatusCode == StatusCode.NotFound)
            {
                logger.LogInformation("No discount available for product {ProductName}.",
                item.ProductName);
            }
            catch (RpcException)
            {
                throw;
            }
        }
    }
}