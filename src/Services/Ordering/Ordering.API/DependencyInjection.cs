using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.Exceptions.Handler;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            _ = services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            string? connectionString = configuration.GetConnectionString("Database");
            services.AddHealthChecks().AddSqlServer(connectionString);
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            _ = app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    }
}