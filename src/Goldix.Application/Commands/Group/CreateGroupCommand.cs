using Goldix.Application.Models.Group;

namespace Goldix.Application.Commands.Group;

public record CreateGroupCommand(CreateUpdateGroupDto dto) : IRequest;
