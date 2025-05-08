using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation($"Integration BasketCheckoutEventHandler {context.Message.GetType().Name}");
            CreateOrderCommand command = context.Message.ToCreateOrderCommand();
            _ = await sender.Send(command);
        }

    }
}
