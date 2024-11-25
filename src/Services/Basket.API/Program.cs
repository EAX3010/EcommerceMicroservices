WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opt => { opt.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache();
#pragma warning restore EXTEXP0018

builder.Services.AddScoped<BasketRepository>();  // Register concrete repository
builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();  // Register decorator as implementation

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
WebApplication app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();