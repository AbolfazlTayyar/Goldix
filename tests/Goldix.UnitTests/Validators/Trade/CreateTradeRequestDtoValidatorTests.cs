using Goldix.Application.Models.Trade;
using Goldix.Application.Validators.Trade;
using Goldix.Domain.Constants;

namespace Goldix.UnitTests.Validators.Trade;

public class CreateTradeRequestDtoValidatorTests
{
    private readonly CreateTradeRequestDtoValidator _validator;

    public CreateTradeRequestDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenProductIdIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductCount = 1,
            ProductPrice = "100.00",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public async Task Handle_WhenProductIdIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 0,
            ProductCount = 1,
            ProductPrice = "100.00",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public async Task Handle_WhenProductCountIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductPrice = "100.00",
            Type = "Buy"
        };
        // Act
        var result = await _validator.TestValidateAsync(dto);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductCount);
    }

    [Fact]
    public async Task Handle_WhenProductCountIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 0,
            ProductPrice = "100.00",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductCount);
    }

    [Fact]
    public async Task Handle_WhenProductPriceIsNotDecimal_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "abc",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductPrice);
    }

    [Fact]
    public async Task Handle_WhenProductPriceIsNegative_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "-100.00",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductPrice);
    }

    [Fact]
    public async Task Handle_WhenProductPriceHasMoreThanTwoDecimalPlaces_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "100.123",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductPrice);
    }

    [Fact]
    public async Task Handle_WhenTypeIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "100.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public async Task Handle_WhenTypeExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "100.00",
            Type = new string('A', DataSchemaConstants.DEFAULT_ENUM_LENGTH + 1)
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public async Task Handle_WhenDtoIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = new CreateTradeRequestDto
        {
            ProductId = 1,
            ProductCount = 1,
            ProductPrice = "100.00",
            Type = "Buy"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
