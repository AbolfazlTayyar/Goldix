using Goldix.Application.Models.Group;

namespace Goldix.Application.Commands.Group;

public record UpdateGroupCommand(int id, CreateUpdateGroupDto dto) : IRequest;