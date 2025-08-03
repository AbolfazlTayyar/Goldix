using Goldix.Application.Models.WalletManagement;

namespace Goldix.Application.Commands.WalletManagement;

public record UpdateWalletBalanceCommand(string id, UpdateWalletBalanceDto dto) : IRequest;
