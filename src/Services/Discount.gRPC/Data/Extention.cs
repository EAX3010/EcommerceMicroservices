namespace Discount.gRPC.Data;

public static class Extention
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MyDBContext>();
        dbContext.Database.MigrateAsync();
        return app;
    }
}