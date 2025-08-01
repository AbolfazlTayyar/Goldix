using Goldix.Application.Models.User;

namespace Goldix.Application.Queries.User;

public record GetAllUsersQuery : IRequest<List<UserDto>>;
