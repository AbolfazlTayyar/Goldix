using Goldix.Application.Queries.Setting;
using Goldix.Domain.Entities.Setting;
using Goldix.Infrastructure.Handlers.QueryHandlers.Notification;
using Goldix.Infrastructure.Persistence;
using Goldix.UnitTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Setting;

public class GetAllSettingsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllSettingsQueryHandler _handler;

    public GetAllSettingsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllSettings()
    {
        // Arrange
        _db.ApplicationSettings.AddRange(new[]
        {
            new ApplicationSetting { SmsApiKey = "TestSmsApiKey" },
        });
        await _db.SaveChangesAsync();

        var query = new GetAllSettingsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Contains(result, s => s.Id == 1 && s.SmsApiKey == "TestSmsApiKey");
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
