﻿using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
              
            //services.AddScoped<ApplicationDbContext>();

            return services;
        }
    }
}
