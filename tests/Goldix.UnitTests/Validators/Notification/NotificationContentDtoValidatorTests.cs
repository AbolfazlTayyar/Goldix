using Goldix.Application.Models.Notification;
using Goldix.Application.Validators.Notification;
using Goldix.Domain.Constants;

namespace Goldix.IntegrationTests.Validators.Notification;

public class NotificationContentDtoValidatorTests
{
    private readonly NotificationContentDtoValidator _validator;

    public NotificationContentDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenSenderIdIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = "",
            Title = "Test Notification",
            Description = "This is a test notification."
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SenderId);
    }

    [Fact]
    public async Task Handle_WhenSenderIdIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = null,
            Title = "Test Notification",
            Description = "This is a test notification."
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SenderId);
    }

    [Fact]
    public async Task Handle_WhenSenderIdExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = new string('A', DataSchemaConstants.DEFAULT_USER_ID_LENGTH + 1),
            Title = "Test Notification",
            Description = "This is a test notification."
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SenderId);
    }

    [Fact]
    public async Task Handle_WhenTitleExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = "user123",
            Title = new string('A', DataSchemaConstants.DEFAULT_STRING_LENGTH + 1),
            Description = "This is a test notification."
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public async Task Handle_WhenDescriptionIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = "user123",
            Title = "Test Notification",
            Description = ""
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public async Task Handle_WhenDescriptionIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            SenderId = "user123",
            Title = "Test Notification",
            Description = null
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public async Task Handle_WhenDtoIsValid_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var dto = new CreateNotificationDto
        {
            Title = "Test Notification",
            SenderId = "user123",
            Description = "This is a test notification."
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert   
        result.ShouldNotHaveAnyValidationErrors();
    }
}