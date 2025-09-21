using Goldix.Application.Models.Product;
using Goldix.Application.Validators.Product;
using Goldix.Domain.Constants;

namespace Goldix.UnitTests.Validators.Product;

public class ProductDtoValidatorTests
{
    private readonly ProductDtoValidator _validator;

    public ProductDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new Application.Models.Product.ProductDto
        {
            Name = "",
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            Name = new string('a', DataSchemaConstants.DEFAULT_NAME_LENGTH + 1),
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            BuyPrice = null,
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceIsNotDecimal_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            BuyPrice = "invalid",
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceIsNegative_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            BuyPrice = "-50.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_When_BuyPriceHasMoreThanTwoDecimalPlaces_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            BuyPrice = "50.123",
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_WhenTradingStartTimeIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            TradingStartTime = TimeSpan.Zero,
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TradingStartTime);
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitIdIsBelowOne_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            MeasurementUnitId = 0,
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MeasurementUnitId);
    }

    [Fact]
    public async Task Handle_WhenIsActiveIsNotSet_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new ProductDto
        {
            // IsActive is a bool and cannot be null, so we skip setting it
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IsActive);
    }
}
