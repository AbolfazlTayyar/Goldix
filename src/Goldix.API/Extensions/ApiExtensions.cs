using Goldix.API.Abstractions;

namespace Goldix.API.Extensions;

public static class ApiExtensions
{
    public static void RegisterEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var endpoints = typeof(Program).Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IEndpointDefinition)) && !x.IsAbstract && !x.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpoint in endpoints)
        {
            endpoint.RegisterEndpoint(app, apiVersionSet);
        }
    }

    public static ApiVersionSet ConfigureApiVersioning(this WebApplication app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(1.0)
            .ReportApiVersions()
            .Build();
    }
}
