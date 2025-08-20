using Goldix.Application.Commands.WalletManagement;
using Goldix.Application.Exceptions;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.WalletManagement;

public class CreateWalletIncreaseRequestCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateWalletIncreaseRequestCommand>
{
    public async Task Handle(CreateWalletIncreaseRequestCommand request, CancellationToken cancellationToken)
    {
        if (!decimal.TryParse(request.dto.Amount, out var amount))
            throw new BadRequestException();

        WalletIncreaseRequest walletIncreaseRequest = new()
        {
            Amount = amount,
            SentAt = DateTime.Now,
            Status = RequestStatus.pending.ToDisplay(),
            SenderId = request.userId
        };

        await db.WalletIncreaseRequests.AddAsync(walletIncreaseRequest, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}