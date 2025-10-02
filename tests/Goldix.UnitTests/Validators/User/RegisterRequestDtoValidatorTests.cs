using Goldix.Application.Models.User.Register;
using Goldix.Application.Validators.User;

namespace Goldix.UnitTests.Validators.User;

public class RegisterRequestDtoValidatorTests
{
    private readonly RegisterRequestDtoValidator _validator;

    public RegisterRequestDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenPasswordDoesNotMatchRePassword_ShouldHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "Password1!",
            RePassword = "DifferentPassword1!"
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.RePassword);
    }


    [Fact]
    public async Task Handle_WhenPasswordDoesNotContainsDigits_ShouldHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "Password",
            RePassword = "Password"
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Handle_WhenPasswordDoesNotContainsLowercase_ShouldHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "PASSWORD1!",
            RePassword = "PASSWORD1!"
        };
        // Act
        var result = await _validator.TestValidateAsync(model);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Handle_WhenPasswordDoesNotContainsUppercase_ShouldHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "password1!",
            RePassword = "password1!"
        };
        // Act
        var result = await _validator.TestValidateAsync(model);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Handle_WhenPasswordDoesNotContainsSpecialCharacter_ShouldHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "Password1",
            RePassword = "Password1"
        };
        // Act
        var result = await _validator.TestValidateAsync(model);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var model = new RegisterRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Password = "Password1!",
            RePassword = "Password1!"
        };

        // Act
        var result = await _validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
