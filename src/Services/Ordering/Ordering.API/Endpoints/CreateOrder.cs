using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto OrderDto);
    public record CreateOrderResponse(Guid OrderId);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            Func<CreateOrderRequest, ISender, Task<IResult>> Handler = async (request, sender) =>
            {
                CreateOrderCommand command = request.Adapt<CreateOrderCommand>();
                CreateOrderResult result = await sender.Send(command);
                CreateOrderResponse response = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.OrderId}", response);
            };
            _ = app.MapPost("/orders", Handler);
            //async Task<IResult> Handle(CreateOrderRequest request, ISender sender)
            //{
            //    CreateOrderCommand command = request.Adapt<CreateOrderCommand>();
            //    CreateOrderResult result = await sender.Send(command);
            //    CreateOrderResponse response = result.Adapt<CreateOrderResponse>();
            //    return Results.Created($"/orders/{response.OrderId}", response);
            //}
        }
    }
}
