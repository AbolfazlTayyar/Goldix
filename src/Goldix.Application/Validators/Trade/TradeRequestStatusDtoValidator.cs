using Goldix.Application.Models.Trade;

namespace Goldix.Application.Validators.Trade;

public class TradeRequestStatusDtoValidator : AbstractValidator<TradeRequestStatusDto>
{
    public TradeRequestStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
