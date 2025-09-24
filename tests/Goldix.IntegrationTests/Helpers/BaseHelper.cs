using Goldix.Application.Models.Group;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers;

public static class BaseHelper
{
    public static IMapper CreateMapper()
    {
        var configExpression = new MapperConfigurationExpression();
        configExpression.AddMaps(typeof(GroupDto).Assembly);
        var config = new MapperConfiguration(configExpression);
        // Don't validate - allows unmapped properties
        return config.CreateMapper();
    }

    public static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }
}