using Goldix.Application.Models.Setting;
using Goldix.Application.Validators.Settings;

namespace Goldix.IntegrationTests.Validators.Setting;

public class CreateSettingsDtoValidatorTests
{
    private readonly CreateSettingsDtoValidator _validator;

    public CreateSettingsDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenSmsApiKeyIsEmpty__ShouldHaveValidationError()
    {
        // Arrange
        var dto = new SettingsDto
        {
            SmsApiKey = string.Empty
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SmsApiKey);
    }
}
