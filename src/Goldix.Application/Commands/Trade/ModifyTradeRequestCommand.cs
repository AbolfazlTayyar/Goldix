using Goldix.Application.Models.Trade;

namespace Goldix.Application.Commands.Trade;

public record ModifyTradeRequestCommand(int id, TradeRequestStatusDto dto) : IRequest;
