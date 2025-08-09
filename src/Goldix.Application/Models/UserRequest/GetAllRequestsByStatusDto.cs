using Goldix.Domain.Enums.User;

namespace Goldix.Application.Models.UserRequest;

public class GetAllRequestsByStatusDto
{
    public UserRequestStatus Status { get; set; }
}
