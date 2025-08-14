using Goldix.Application.Models.Group;

namespace Goldix.Application.Queries.Group;

public record GetGroupByIdQuery(int id) : IRequest<GroupDto>;
