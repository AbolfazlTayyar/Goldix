using Goldix.Application.Models.User.GetToken;
using Goldix.Application.Validators.User;
using Goldix.Domain.Constants;

namespace Goldix.UnitTests.Validators.User;

public class GetTokenRequestDtoValidatorTests
{
    private readonly GetTokenRequestDtoValidator _validator;

    public GetTokenRequestDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenUserNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var model = new GetTokenRequestDto
        {
            UserName = string.Empty,
            Password = "ValidPassword123!"
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public async Task Handle_WhenUserNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var model = new GetTokenRequestDto
        {
            UserName = new string('a', DataSchemaConstants.DEFAULT_STRING_LENGTH + 1),
            Password = "ValidPassword123!"
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var model = new GetTokenRequestDto
        {
            UserName = "ValidUserName",
            Password = string.Empty
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsBelowMinLength_ShouldHaveValidationError()
    {
        // Arrange
        var model = new GetTokenRequestDto
        {
            UserName = "ValidUserName",
            Password = new string('a', DataSchemaConstants.DEFAULT_PASSWORD_LENGTH - 1)
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}
