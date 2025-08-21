using Goldix.Application.Models.WalletManagement;

namespace Goldix.Application.Commands.WalletManagement;

public record UpdateWalletIncreaseRequestStatusCommand(int id, UpdateWalletIncreaseRequestStatusDto dto) : IRequest;
