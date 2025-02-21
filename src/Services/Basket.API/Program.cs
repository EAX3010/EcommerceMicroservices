#region

using Discount.gRPC;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    _ = config.RegisterServicesFromAssemblies(assembly);
    _ = config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    _ = config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opt => { opt.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache(op =>
{
    op.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(5),
        LocalCacheExpiration = TimeSpan.FromMinutes(1)
    };
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
});
#pragma warning restore EXTEXP0018

builder.Services.AddScoped<IBasketRepository, BasketRepository>(); // Register concrete repository
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>(); // Register decorator as implementation

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("RedisConnectionString")!);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcConfigs:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});


WebApplication app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(_ => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();