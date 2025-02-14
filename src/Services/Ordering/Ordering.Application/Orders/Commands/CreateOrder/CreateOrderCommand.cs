using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderResult(Guid orderId);
    public record CreateOrderCommand(OrderDto orderDto) : ICommand<CreateOrderResult>;

    public class CreateProductCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateProductCommandValidator()
        {
            _ = RuleFor(x => x.orderDto.OrderName)
                .NotEmpty().WithMessage("OrderName is required");

            _ = RuleFor(x => x.orderDto.CustomerId)
                .NotEmpty().NotNull().WithMessage("CustomerId is required");

            _ = RuleFor(x => x.orderDto.OrderItems)
                 .NotEmpty().NotNull().WithMessage("OrderItems is required");

        }
    }

}
