using Goldix.Application.Models.Product;
using Goldix.Application.Validators.Product;
using Goldix.Domain.Constants;

namespace Goldix.IntegrationTests.Validators.Product;

public class CreateMeasurementUnitDtoValidatorTests
{
    private readonly CreateMeasurementUnitDtoValidator _validator;

    public CreateMeasurementUnitDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        MeasurementUnitDto dto = new();

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        MeasurementUnitDto dto = new()
        {
            Name = new string('a', DataSchemaConstants.DEFAULT_STRING_LENGTH + 1)
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenDtoIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        MeasurementUnitDto dto = new()
        {
            Name = "Valid Name"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}
