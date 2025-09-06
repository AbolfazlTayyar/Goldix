using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Group;

public class ModifyGroupMembersCommandHandler(ApplicationDbContext db) : IRequestHandler<ModifyGroupMembersCommand>
{
    public async Task Handle(ModifyGroupMembersCommand request, CancellationToken cancellationToken)
    {
        if (db.Database.IsInMemory()) // for testing purposes   
        {
            await Operation(request, cancellationToken);
        }
        else
        {
            using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
            await Operation(request, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
    }

    private async Task Operation(ModifyGroupMembersCommand request, CancellationToken cancellationToken)
    {
        var allPhoneNumbers = new HashSet<string>();

        if (request.dto.UsersToAdd?.Any() == true)
            allPhoneNumbers.UnionWith(request.dto.UsersToAdd);

        if (request.dto.UsersToRemove?.Any() == true)
            allPhoneNumbers.UnionWith(request.dto.UsersToRemove);

        if (!allPhoneNumbers.Any())
            throw new BadRequestException();

        bool isGroupExist = await db.Groups.AnyAsync(x => x.Id == request.id, cancellationToken);
        if (!isGroupExist)
            throw new NotFoundException();

        var userStatus = UserStatus.confirmed.ToDisplay();
        var users = await db.Users
            .Where(x => allPhoneNumbers.Contains(x.PhoneNumber) && x.Status == userStatus)
            .ToListAsync(cancellationToken);

        if (users.Count <= 0)
            throw new NotFoundException();

        var userLookup = users.ToDictionary(u => u.PhoneNumber, u => u);

        if (request.dto.UsersToAdd?.Any() == true)
        {
            foreach (var phoneNumber in request.dto.UsersToAdd)
            {
                if (userLookup.TryGetValue(phoneNumber, out var user))
                    user.GroupId = request.id;
            }
        }

        if (request.dto.UsersToRemove?.Any() == true)
        {
            foreach (var phoneNumber in request.dto.UsersToRemove)
            {
                if (userLookup.TryGetValue(phoneNumber, out var user))
                    user.GroupId = null;
            }
        }

        await db.SaveChangesAsync(cancellationToken);
    }
}
