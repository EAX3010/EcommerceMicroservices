using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Shared.Messaging.MassTransit;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _ = services.AddMediatR(config =>
            {
                _ = config.RegisterServicesFromAssembly(assembly);
                _ = config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                _ = config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            _ = services.AddValidatorsFromAssembly(assembly);
            _ = services.AddFeatureManagement();
            _ = services.AddMessageBroker(configuration, assembly);
            return services;
        }
    }
}