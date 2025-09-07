using Goldix.Application.Exceptions;
using Goldix.Application.Queries.Group;
using Goldix.Domain.Constants;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.QueryHandlers.Group;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Group;
using Goldix.UnitTests.Helpers;
using Goldix.UnitTests.Helpers.User;

namespace Goldix.UnitTests.Handlers.QueryHandlers.Group;

public class GetAllUsersGroupQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly GetAllUsersGroupQueryHandler _handler;

    public GetAllUsersGroupQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();

        var store = new Mock<IRoleStore<IdentityRole>>();
        _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
            store.Object, null, null, null, null
        );

        _handler = new GetAllUsersGroupQueryHandler(_db, _mockRoleManager.Object);
    }

    [Fact]
    public async Task Handle_WhenDidNotFoundGroup_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetAllUsersGroupQuery(id: 1, page: 1, pageSize: 10);

        _mockRoleManager.Setup(rm => rm.FindByNameAsync("User"))
            .ReturnsAsync(new IdentityRole("User"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenUserRoleNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsAsync(_db, 1);

        var query = new GetAllUsersGroupQuery(id: 1, page: 1, pageSize: 10);

        _mockRoleManager.Setup(rm => rm.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((IdentityRole)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenNoUsersHaveUserRole_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsAsync(_db, 1);

        await UserTestHelper.SeedRolesAsync(_db);

        _mockRoleManager.Setup(rm => rm.FindByNameAsync("User"))
            .ReturnsAsync(new IdentityRole("User"));

        var users = await UserTestHelper.SeedUsersAsync(_db, 5, status: "confirmed", isActive: true);

        await UserTestHelper.AddUsersToRole(_db, users, "Admin");

        var query = new GetAllUsersGroupQuery(id: 1, page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenValidRequestWithUsersInUserRole_ShouldReturnPagedUserGroupDto()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsAsync(_db, 3);

        await UserTestHelper.SeedRolesAsync(_db);

        _mockRoleManager.Setup(rm => rm.FindByNameAsync(RoleConstants.USER))
            .ReturnsAsync(new IdentityRole(RoleConstants.USER)
            {
                Id = "1"
            });

        var users = await UserTestHelper.SeedUsersAsync(_db, 5, status: UserStatus.confirmed.ToDisplay(), isActive: true);
        await UserTestHelper.AddUsersToRole(_db, users, RoleConstants.USER);

        var query = new GetAllUsersGroupQuery(id: 1, page: 1, pageSize: 3);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Items.Count());
        Assert.Equal(5, result.TotalCount);

        var firstReturnedUser = result.Items.First();
        Assert.Equal("First1 Last1", firstReturnedUser.FullName);
        Assert.Equal("09000000001", firstReturnedUser.PhoneNumber);
        Assert.Equal("Group 1", firstReturnedUser.GroupName);
        Assert.True(firstReturnedUser.IsInGroup);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
