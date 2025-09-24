using Goldix.Application.Commands.Setting;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Setting;
using Goldix.Domain.Entities.Setting;
using Goldix.Infrastructure.Handlers.CommandHandlers.Setting;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Setting;

public class CreateSettingsCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly CreateSettingsCommandHandler _handler;

    public CreateSettingsCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenSettingsExist_ShouldThrowException()
    {
        // Arrange
        _db.ApplicationSettings.Add(new ApplicationSetting
        {
            SmsApiKey = "ExistingKey",
        });

        await _db.SaveChangesAsync();

        var command = new CreateSettingsCommand
        (
            new SettingsDto
            {
                SmsApiKey = "NewKey"
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() =>
                _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldCreateSettings()
    {
        // Arrange
        var command = new CreateSettingsCommand
        (
            new SettingsDto
            {
                SmsApiKey = "TestKey",
            }
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var settings = await _db.ApplicationSettings.FirstOrDefaultAsync(s => s.SmsApiKey == "TestKey");
        Assert.NotNull(settings);
        Assert.Equal("TestKey", settings.SmsApiKey);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
