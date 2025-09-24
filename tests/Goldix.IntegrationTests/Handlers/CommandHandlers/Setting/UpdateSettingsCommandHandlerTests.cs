using Goldix.Application.Commands.Setting;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Setting;
using Goldix.Domain.Entities.Setting;
using Goldix.Infrastructure.Handlers.CommandHandlers.Setting;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Setting;

public class UpdateSettingsCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly UpdateSettingsCommandHandler _handler;

    public UpdateSettingsCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenSettingsDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateSettingsCommand(new SettingsDto());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenSettingsExist_ShouldUpdateSettings()
    {
        // Arrange
        _db.ApplicationSettings.Add(new ApplicationSetting
        {
            SmsApiKey = "ExistingKey",
        });
        await _db.SaveChangesAsync();

        var command = new UpdateSettingsCommand(new SettingsDto
        {
            SmsApiKey = "UpdatedKey"
        });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedSettings = await _db.ApplicationSettings.FirstOrDefaultAsync();
        Assert.NotNull(updatedSettings);
        Assert.Equal("UpdatedKey", updatedSettings.SmsApiKey);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
