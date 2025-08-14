using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Group;

public class DeleteGroupCommandHandler(ApplicationDbContext db) : IRequestHandler<DeleteGroupCommand>
{
    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await db.Groups.FirstOrDefaultAsync(x=>x.Id == request.id, cancellationToken);
        if (group is null)
            throw new NotFoundException();

        db.Groups.Remove(group);
        await db.SaveChangesAsync(cancellationToken);    
    }
}
