using Goldix.Domain.Enums.Common;

namespace Goldix.Application.Models.WalletManagement;

public class UpdateWalletIncreaseRequestStatusDto
{
    public RequestStatus Status { get; set; }
    public string AdminId { get; set; }
}
