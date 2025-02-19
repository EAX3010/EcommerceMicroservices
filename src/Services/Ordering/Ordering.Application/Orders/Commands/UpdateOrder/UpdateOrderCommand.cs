using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto orderDto) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        _ = RuleFor(x => x.orderDto.OrderName)
            .NotEmpty().WithMessage("OrderName is required");

        _ = RuleFor(x => x.orderDto.CustomerId)
            .NotEmpty().NotNull().WithMessage("CustomerId is required");

        _ = RuleFor(x => x.orderDto.OrderItems)
            .NotEmpty().NotNull().WithMessage("OrderItems is required");
    }
}