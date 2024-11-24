
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess = false);

    public class DeleteBasketValidatior : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidatior()
        {
            _ = RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            bool result = await repository.DeleteBasket(command.Username, cancellationToken);
            return new DeleteBasketResult(result);
        }
    }
}