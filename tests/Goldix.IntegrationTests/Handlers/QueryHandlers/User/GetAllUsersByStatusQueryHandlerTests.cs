using Goldix.Application.Models.User;
using Goldix.Application.Queries.User;
using Goldix.Domain.Constants;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.QueryHandlers.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.User;

public class GetAllUsersByStatusQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly IMapper _mapper;
    private readonly GetAllUsersByStatusQueryHandler _handler;

    public GetAllUsersByStatusQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mockRoleManager = BaseHelper.MockRoleManager();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_mockRoleManager.Object, _db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoUsersWithGivenStatus_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        _mockRoleManager.Setup(r => r.FindByNameAsync(RoleConstants.USER))
            .ReturnsAsync(new IdentityRole
            {
                Id = "role-id",
                Name = "User"
            });

        var query = new GetAllUsersByStatusQuery(new UserStatusDto
        {
            Status = UserStatus.confirmed
        }, page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenUsersWithGivenStatusExist_ShouldReturnPagedResult()
    {
        // Arrange
        await UserTestHelper.SeedRolesAsync(_db);

        var users = await UserTestHelper.SeedUsersAsync(_db, 5, UserStatus.confirmed.ToDisplay());
        await UserTestHelper.AddUsersToRole(_db, users, RoleConstants.USER);

        var firstUserRoleId = _db.UserRoles.First().RoleId;
        _mockRoleManager.Setup(r => r.FindByNameAsync(RoleConstants.USER))
            .ReturnsAsync(new IdentityRole
            {
                Id = firstUserRoleId,
                Name = "User"
            });

        var query = new GetAllUsersByStatusQuery(new UserStatusDto
        {
            Status = UserStatus.confirmed
        }, page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Items.Count());
        Assert.Equal(5, result.TotalCount);

        var firstItem = result.Items.First();
        Assert.Equal("First1", firstItem.FirstName);
        Assert.Equal("Last1", firstItem.LastName);
        Assert.Equal("09000000001", firstItem.UserName);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
