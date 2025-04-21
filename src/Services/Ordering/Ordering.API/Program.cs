namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            _ = builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();


            WebApplication app = builder.Build();
            _ = app.UseApiServices();
            if (app.Environment.IsDevelopment())
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    _ = scope.InitializeDatabase();
                }
            }

            app.Run();
        }
    }
}