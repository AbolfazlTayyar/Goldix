using Goldix.Application.Commands.User;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.User;

public class ModifyUserStatusCommandHandler(ApplicationDbContext db) : IRequestHandler<ModifyUserStatusCommand>
{
    public async Task Handle(ModifyUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (user is null)
            throw new NotFoundException();

        user.Status = request.dto.Status.ToDisplay();
        await db.SaveChangesAsync(cancellationToken);
    }
}
