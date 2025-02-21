#region

using FluentValidation;

#endregion

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            _ = RuleFor(x => x.OrderDto.OrderName)
                .NotEmpty().WithMessage("OrderName is required");

            _ = RuleFor(x => x.OrderDto.CustomerId)
                .NotEmpty().NotNull().WithMessage("CustomerId is required");

            _ = RuleFor(x => x.OrderDto.OrderItems)
                .NotEmpty().NotNull().WithMessage("OrderItems is required");
        }
    }
}