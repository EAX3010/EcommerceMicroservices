#region

using Ordering.Application.Data;
using Ordering.Infrastructure.Data.Interceptors;

#endregion

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            _ = services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            _ = services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            _ = services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                _ = options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                _ = options.UseSqlServer(connectionString);
            });

            _ = services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }
}