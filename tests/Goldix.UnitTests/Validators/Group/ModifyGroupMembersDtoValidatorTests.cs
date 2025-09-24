using Goldix.Application.Models.Group;
using Goldix.Application.Validators.Group;

namespace Goldix.IntegrationTests.Validators.Group;

public class ModifyGroupMembersDtoValidatorTests
{
    private readonly ModifyGroupMembersDtoValidator _validator;

    public ModifyGroupMembersDtoValidatorTests()
    {
        _validator = new ModifyGroupMembersDtoValidator();
    }

    [Fact]
    public async Task Validate_WhenNoUsersToAddOrRemove_ShouldHaveValidationErrorAsync()
    {
        // Arrange
        var dto = new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string>(),
            UsersToRemove = new List<string>()
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x);
    }

    [Fact]
    public async Task Validate_WhenUsersToAdd_ShouldNotHaveValidationErrorAsync()
    {
        // Arrange
        var dto = new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string> { "user1" },
            UsersToRemove = new List<string>()
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }

    [Fact]
    public async Task Validate_WhenUsersToRemove_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string>(),
            UsersToRemove = new List<string> { "user1" }
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }
}
