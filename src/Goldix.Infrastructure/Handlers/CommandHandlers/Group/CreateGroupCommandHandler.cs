using Goldix.Application.Commands.Group;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Group;

public class CreateGroupCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateGroupCommand>
{
    public async Task Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = mapper.Map<Domain.Entities.User.Group>(request.dto);

        group.CreatedAt = DateTime.Now;

        await db.Groups.AddAsync(group, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
