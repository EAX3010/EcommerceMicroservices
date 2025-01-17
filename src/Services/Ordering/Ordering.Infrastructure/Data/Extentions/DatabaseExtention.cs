using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data.Extentions
{
    public static class DatabaseExtention
    {
        public static async Task InitializeDatabase(this IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    await SeedAsync(context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await DatabaseSeed.GenerateCustomers(context, 10);
            await DatabaseSeed.GenerateProducts(context, 10);
            await DatabaseSeed.GenerateOrders(context, 10);
        }

    }
}
