var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<MyDBContext>(p => 
{
    p.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();
app.UseMigration(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();