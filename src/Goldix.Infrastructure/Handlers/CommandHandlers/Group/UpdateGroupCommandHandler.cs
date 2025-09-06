using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Group;

public record UpdateGroupCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<UpdateGroupCommand>
{
    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await db.Groups.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (group == null)
            throw new NotFoundException();

        mapper.Map(request.dto, group);
        await db.SaveChangesAsync(cancellationToken);
    }
}