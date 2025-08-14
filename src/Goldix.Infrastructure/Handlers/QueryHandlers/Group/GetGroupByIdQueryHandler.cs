using Goldix.Application.Exceptions;
using Goldix.Application.Models.Group;
using Goldix.Application.Queries.Group;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Group;

public class GetGroupByIdQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetGroupByIdQuery, GroupDto>
{
    public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await db.Groups.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (group is null)
            throw new NotFoundException();

        var result = mapper.Map<GroupDto>(group);

        return result;
    }
}
