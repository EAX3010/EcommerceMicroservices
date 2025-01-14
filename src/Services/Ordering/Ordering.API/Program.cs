using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;
namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
       
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();


            var app = builder.Build();
            app.UseApiServices();
            if(app.Environment.IsDevelopment())
            {
                app.Services.CreateScope().InitializeDatabase();
            }
            app.Run();
        }
    }
}

