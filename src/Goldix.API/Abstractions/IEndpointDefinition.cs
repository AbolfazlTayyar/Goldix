namespace Goldix.API.Abstractions;

public interface IEndpointDefinition
{
    void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet);
}
