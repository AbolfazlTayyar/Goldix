using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Group;
using Goldix.Application.Models.Group;
using Goldix.Application.Queries.Group;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class GroupEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var group = app.MapGroup("/api/v{version:apiVersion}/groups")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0)
            .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        group.MapPost("", async (CreateUpdateGroupDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateGroupCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateUpdateGroupDto>>();

        group.MapPut("{id:int:min(1)}", async (int id, CreateUpdateGroupDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateGroupCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateUpdateGroupDto>>();

        group.MapDelete("{id:int:min(1)}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeleteGroupCommand(id), cancellationToken);
            return ApiResponse.Ok();
        });

        group.MapGet("{id:int:min(1)}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetGroupByIdQuery(id), cancellationToken);

            return ApiResponse.Ok(result);
        });

        group.MapGet("", async ([FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllGroupsQuery(page, pageSize), cancellationToken);

            return ApiResponse.Ok(result);
        });

        group.MapPatch("{id:int:min(1)}/members", async (int id, ModifyGroupMembersDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new ModifyGroupMembersCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<ModifyGroupMembersDto>>();

        group.MapGet("{id:int:min(1)}/members", async (int id, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllUsersGroupQuery(id, page, pageSize), cancellationToken);

            return ApiResponse.Ok(result);
        });
    }
}
