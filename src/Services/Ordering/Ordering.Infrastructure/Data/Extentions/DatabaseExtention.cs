using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }
    }
}
