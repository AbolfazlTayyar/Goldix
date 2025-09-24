using Goldix.Application.Models.Product;
using Goldix.Application.Validators.Product;

namespace Goldix.IntegrationTests.Validators.Product;

public class PriceDtoValidatorTests
{
    private readonly PricingDtoValidator _validator;

    public PriceDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenBuyPriceIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new PricingDto
        {
            BuyPrice = null,
            SellPrice = "100.00"
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
        var dto = new PricingDto
        {
            BuyPrice = "invalid",
            SellPrice = "100.00"
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
        var dto = new PricingDto
        {
            BuyPrice = "-50.00",
            SellPrice = "100.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceHasMoreThanTwoDecimalPlaces_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new PricingDto
        {
            BuyPrice = "50.123",
            SellPrice = "100.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPrice);
    }

    [Fact]
    public async Task Handle_WhenDtoIsValid_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var dto = new PricingDto
        {
            BuyPrice = "50.00",
            SellPrice = "100.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
