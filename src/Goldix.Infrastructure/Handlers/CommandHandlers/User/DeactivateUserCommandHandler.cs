using Goldix.Application.Commands.User;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.User;

public class DeactivateUserCommandHandler(ApplicationDbContext db) : IRequestHandler<DeactivateUserCommand>
{
    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var record = await db.Users.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (record is null)
            throw new NotFoundException();

        record.IsActive = false;
        await db.SaveChangesAsync(cancellationToken);
    }
}
