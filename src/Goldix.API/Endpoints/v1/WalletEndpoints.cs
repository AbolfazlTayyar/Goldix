using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.WalletManagement;
using Goldix.Application.Models.WalletManagement;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class WalletEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var wallet = app.MapGroup("/api/v{version:apiVersion}/wallets")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0);

        wallet.MapPatch("{id}/balance", async (string id, UpdateWalletBalanceDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateWalletBalanceCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<UpdateWalletBalanceDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        wallet.MapPost("/{userId}/increase-requests", async (string userId, WalletIncreaseRequestDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateWalletIncreaseRequestCommand(userId, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<WalletIncreaseRequestDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.USER));

        // 'id' is the identifier of the WalletIncreaseRequest
        wallet.MapPut("/increase-requests/{id}/status", async (int id, UpdateWalletIncreaseRequestStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateWalletIncreaseRequestStatusCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<UpdateWalletIncreaseRequestStatusDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}
