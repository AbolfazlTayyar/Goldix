using Goldix.Application.Models.Group;

namespace Goldix.Application.Commands.Group;

public record ModifyGroupMembersCommand(int id, ModifyGroupMembersDto dto) : IRequest;
