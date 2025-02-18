using FluentValidation;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteProductCommandValidator()
        {
            _ = RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order Id is required");

        }
    }

}
