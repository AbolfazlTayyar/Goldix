using Goldix.Application.Models.WalletManagement;

namespace Goldix.Application.Commands.WalletManagement;

public record CreateWalletIncreaseRequestCommand(string userId, WalletIncreaseRequestDto dto) : IRequest;
