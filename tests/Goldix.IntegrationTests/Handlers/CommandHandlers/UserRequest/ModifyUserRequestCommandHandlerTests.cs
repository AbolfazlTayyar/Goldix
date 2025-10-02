using Goldix.Application.Commands.Trade;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.CommandHandlers.UserRequest;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.UserRequest;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.UserRequest;

public class ModifyUserRequestCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly ModifyUserRequestCommandHandler _handler;

    public ModifyUserRequestCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new(_db);
    }

    [Fact]
    public async Task Handle_WhenTradeRequestDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new ModifyTradeRequestCommand(1, new TradeRequestStatusDto());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenTradeRequestExists_ShouldModifyStatus()
    {
        // Arrange
        var requests = await UserRequestTestHelper.CreateUserRequestsAsync(_db, 1);

        var command = new ModifyTradeRequestCommand(1, new TradeRequestStatusDto { Status = RequestStatus.rejected });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedRequest = await _db.TradeRequests.FindAsync(1);
        Assert.NotNull(updatedRequest);
        Assert.Equal(RequestStatus.rejected.ToDisplay(), updatedRequest.Status);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
