#region

using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data.Extensions;

#endregion

namespace Ordering.Infrastructure.Data.Extentions
{
    public static class DatabaseExtention
    {
        public static async Task InitializeDatabase(this IServiceScope scope)
        {
            IServiceProvider services = scope.ServiceProvider;
            ILogger<ApplicationDbContext> logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
            try
            {
                ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                    await DatabaseSeed.GenerateCustomers(context, 10);
                    await DatabaseSeed.GenerateProducts(context, 10);
                    await DatabaseSeed.GenerateOrders(context, 10);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }

    }
}