using Goldix.Application.Commands.Trade;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.CommandHandlers.Trade;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Trade;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Trade;

public class ModifyTradeRequestCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly ModifyTradeRequestCommandHandler _handler;

    public ModifyTradeRequestCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new(_db);
    }

    [Fact]
    public async Task Handle_WhenTradeRequestDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new ModifyTradeRequestCommand(1, new ModifyTradeRequestDto
        {
            ProductCount = 5,
            ProductPrice = "150.75",
            Type = "فروش"
        });

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldUpdateTradeRequestStatus()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 1, RequestStatus.pending, DateTime.Now);

        var command = new ModifyTradeRequestCommand(1, new ModifyTradeRequestDto
        {
            ProductCount = 5,
            ProductPrice = "150.75",
            Type = "فروش",
            Status = RequestStatus.confirmed
        });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedTradeRequest = await _db.TradeRequests.FindAsync(1);
        Assert.NotNull(updatedTradeRequest);
        Assert.Equal(RequestStatus.confirmed.ToDisplay(), updatedTradeRequest.Status);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
