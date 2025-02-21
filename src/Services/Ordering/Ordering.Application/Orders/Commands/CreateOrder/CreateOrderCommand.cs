#region

using FluentValidation;

#endregion

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid OrderId);

    public class CreateProductCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateProductCommandValidator()
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