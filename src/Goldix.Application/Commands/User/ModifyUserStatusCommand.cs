using Goldix.Application.Models.User;

namespace Goldix.Application.Commands.User;

public record ModifyUserStatusCommand(string id, UserStatusDto dto) : IRequest;
