using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Product;
using Goldix.Application.Models.Pagination;
using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class ProductEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var product = app.MapGroup("/api/v{version:apiVersion}/products")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0);

        product.MapGet("", async ([FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetAllProductsQuery(pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        product.MapGet("{id:int:min(1)}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        product.MapPost("", async (ProductDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateProductCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<ProductDto>>()
         .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        product.MapPut("{id:int:min(1)}", async (int id, ProductDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateProductCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<ProductDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        product.MapDelete("{id:int:min(1)}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeleteProductCommand(id), cancellationToken);

            return ApiResponse.Ok();
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}
