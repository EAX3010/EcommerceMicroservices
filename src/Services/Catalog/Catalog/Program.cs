
namespace Catalog.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //dj


            var app = builder.Build();
            //pipelines
            app.Run();
        }
    }
}