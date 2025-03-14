﻿#region

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
            await DatabaseSeed.GenerateCustomers(context, 300);
            await DatabaseSeed.GenerateProducts(context, 1000);
            await DatabaseSeed.GenerateOrders(context, 1200);
        }
    }
}