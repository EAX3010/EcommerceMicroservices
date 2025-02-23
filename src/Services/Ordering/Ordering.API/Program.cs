namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();


            WebApplication app = builder.Build();
            app.UseApiServices();
            if (app.Environment.IsDevelopment())
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    using Task _ = scope.InitializeDatabase();
                }
            }

            app.Run();
        }
    }
}