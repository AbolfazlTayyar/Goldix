using Goldix.Application.Models.UserRequest;

namespace Goldix.Application.Commands.UserRequest;

public record ModifyUserRequestCommand(int id, UserStatusDto dto) : IRequest;
