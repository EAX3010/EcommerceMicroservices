namespace Discount.gRPC.Data
{
    public static class Extention
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using MyDBContext dbContext = scope.ServiceProvider.GetRequiredService<MyDBContext>();
            dbContext.Database.MigrateAsync();
            return app;
        }
    }
}