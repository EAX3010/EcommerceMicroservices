#region

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


System.Reflection.Assembly assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    _ = config.RegisterServicesFromAssemblies(assembly);
    _ = config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    _ = config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opt => { opt.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();
if (builder.Environment.IsDevelopment())
{
    _ = builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


WebApplication app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();