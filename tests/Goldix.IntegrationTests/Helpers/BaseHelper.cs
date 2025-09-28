using Goldix.Application.Models.Group;
using Goldix.Domain.Entities.User;
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

    public static Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());
        return userManager;
    }

    public static Mock<RoleManager<IdentityRole>> MockRoleManager()
    {
        var store = new Mock<IRoleStore<IdentityRole>>();
        var roleManager = new Mock<RoleManager<IdentityRole>>(
            store.Object, null, null, null, null
        );
        return roleManager;
    }
}