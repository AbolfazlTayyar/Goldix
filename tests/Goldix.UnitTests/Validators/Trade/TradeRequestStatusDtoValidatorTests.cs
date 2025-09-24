using Goldix.Application.Models.Trade;
using Goldix.Application.Validators.Trade;
using Goldix.Domain.Enums.Common;

namespace Goldix.UnitTests.Validators.Trade;

public class TradeRequestStatusDtoValidatorTests
{
    private readonly TradeRequestStatusDtoValidator _validator;

    public TradeRequestStatusDtoValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Handle_WhenStatusIsNotInEnum_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new TradeRequestStatusDto
        {
            Status = (RequestStatus)999
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [Fact]
    public async Task Hanlde_WhenDtoIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = new TradeRequestStatusDto
        {
            Status = RequestStatus.confirmed
        };

        // Act
        var result = await _validator.TestValidateAsync(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }
}
