using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"OrderCreatedEventHandler {domainEvent.GetType().Name}");
            if (featureManager.IsEnabledAsync("OrderFullfilment").Result)
            {
                logger.LogInformation($"OrderCreatedIntegrationEvent is enabled");
                var orderCreatedIntegrationEvent = domainEvent.Order.ToDto();
                publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
            else
            {
                logger.LogInformation($"OrderCreatedIntegrationEvent is disabled");
            }
        }
    }
}