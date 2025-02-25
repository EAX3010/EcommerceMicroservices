﻿using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            _ = services.AddCarter();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            _ = app.MapCarter();
            return app;
        }
    }
}