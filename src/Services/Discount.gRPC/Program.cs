var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<MyDBContext>(p =>
{
    p.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

WebApplication app = builder.Build();
app.UseMigration(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.MapGrpcService<DiscountService>();
app.Run();