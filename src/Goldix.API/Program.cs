using Goldix.API.Extensions;
using Goldix.Application;
using Goldix.Infrastructure;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddAntiforgery();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApiVersioning();
builder.Services.AddAuthorization();

// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAntiforgery();

app.UseHttpsRedirection();

app.UseTokenDecryption();
app.UseGlobalExceptionHandling();

app.UseAuthentication();
app.UseAuthorization();

await app.ApplyMigrationsIfPending();
await app.SeedData();

var apiVersionSet = app.ConfigureApiVersioning();
app.RegisterEndpoints(apiVersionSet);

app.Run();