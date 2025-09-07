using Goldix.Application.Models.Group;
using Goldix.Application.Validators.Group;

namespace Goldix.UnitTests.Validators.Group;

public class GroupDtoValidatorTests
{
    private readonly GroupDtoValidator _validator;

    public GroupDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "",
            BuyPriceDifferencePercent = "10.00",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenNameIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = null,
            BuyPriceDifferencePercent = "10.00",
            SellPriceDifferencePercent = "15.00"
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
        var dto = new CreateUpdateGroupDto
        {
            Name = new string('A', 256),
            BuyPriceDifferencePercent = "10.00",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceDifferencePercentIsInvalidDecimal_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "Valid Name",
            BuyPriceDifferencePercent = "invalid",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPriceDifferencePercent);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceDifferencePercentIsNegative_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "Valid Name",
            BuyPriceDifferencePercent = "-5.00",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPriceDifferencePercent);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceDifferencePercentHasTooManyDecimalPlaces_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "Valid Name",
            BuyPriceDifferencePercent = "10.123",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuyPriceDifferencePercent);
    }

    [Fact]
    public async Task Handle_WhenBuyPriceIsZero_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "Valid Name",
            BuyPriceDifferencePercent = "0.00",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Handle_WhenDtoIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = new CreateUpdateGroupDto
        {
            Name = "Valid Name",
            BuyPriceDifferencePercent = "10.00",
            SellPriceDifferencePercent = "15.00"
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
