using Goldix.Application.Commands.Trade;
using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.Trade;
using Goldix.Infrastructure.Handlers.CommandHandlers.Trade;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Trade;

public class CreateTradeRequestCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly Mock<IUserService> _userService;
    private readonly CreateTradeRequestCommandHandler _handler;

    public CreateTradeRequestCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _userService = new Mock<IUserService>();
        _handler = new(_db, _userService.Object);
    }

    [Fact]
    public async Task Handle_WhenProductPriceIsNotValidDecimal_ShouldThrowBadRequestException()
    {
        // Arrange
        var command = new CreateTradeRequestCommand
        (
            new CreateTradeRequestDto
            {
                ProductId = 1,
                ProductCount = 2,
                ProductPrice = "invalid_decimal",
                Type = "خرید"
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        _userService.Setup(x => x.GetCurrentUserId()).Returns("non_existing_user_id");

        var command = new CreateTradeRequestCommand
        (
            new CreateTradeRequestDto
            {
                ProductId = 1,
                ProductCount = 2,
                ProductPrice = "100.50",
                Type = "خرید"
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotHaveWallet_ShouldThrowBadRequestException()
    {
        // Arrange
        await UserTestHelper.CreateUsersAsync(_db, 1);

        _userService.Setup(x => x.GetCurrentUserId()).Returns("1");

        var command = new CreateTradeRequestCommand
        (
            new CreateTradeRequestDto
            {
                ProductId = 1,
                ProductCount = 2,
                ProductPrice = "100.50",
                Type = "خرید"
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenTotalPriceIsMoreThanWalletBalance_ShouldThrowBadRequestException()
    {
        // Arrange
        await UserTestHelper.CreateUsersAsync(_db, 1, shouldHaveWallet: true);

        _userService.Setup(x => x.GetCurrentUserId()).Returns("1");

        var command = new CreateTradeRequestCommand
        (
            new CreateTradeRequestDto
            {
                ProductId = 1,
                ProductCount = 2,
                ProductPrice = "100000.50",
                Type = "خرید"
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldCreateTradeRequest()
    {
        // Arrange
        await UserTestHelper.CreateUsersAsync(_db, 1, shouldHaveWallet: true);

        _userService.Setup(x => x.GetCurrentUserId()).Returns("1");

        var command = new CreateTradeRequestCommand
        (
            new CreateTradeRequestDto
            {
                ProductId = 1,
                ProductCount = 2,
                ProductPrice = "100.50",
                Type = "خرید"
            }
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var tradeRequest = await _db.TradeRequests.FirstOrDefaultAsync();
        Assert.NotNull(tradeRequest);
        Assert.Equal(1, tradeRequest.ProductId);
        Assert.Equal(2, tradeRequest.ProductCount);
        Assert.Equal(100.50m, tradeRequest.ProductPrice);
        Assert.Equal(201.00m, tradeRequest.ProductTotalPrice);
        Assert.Equal("خرید", tradeRequest.Type);
        Assert.Equal("در صف انتظار", tradeRequest.Status);
        Assert.Equal("1", tradeRequest.SenderId);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
