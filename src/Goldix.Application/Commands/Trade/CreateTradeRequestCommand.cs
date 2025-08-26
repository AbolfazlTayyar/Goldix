using Goldix.Application.Models.Trade;

namespace Goldix.Application.Commands.Trade;

public record CreateTradeRequestCommand(CreateTradeRequestDto dto) : IRequest;
