using Goldix.Application.Commands.WalletManagement;
using Goldix.Application.Exceptions;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Domain.Enums.Common;
using Goldix.Domain.Enums.WalletManagement;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.WalletManagement;

public class UpdateWalletIncreaseRequestStatusCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateWalletIncreaseRequestStatusCommand>
{
    public async Task Handle(UpdateWalletIncreaseRequestStatusCommand request, CancellationToken cancellationToken)
    {
        var walletIncreaseRequest = await db.WalletIncreaseRequests.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (walletIncreaseRequest is null)
            throw new NotFoundException();

        var status = request.dto.Status.ToDisplay();

        walletIncreaseRequest.Status = status;
        walletIncreaseRequest.RespondedAt = DateTime.Now;
        walletIncreaseRequest.IsRead = true;
        walletIncreaseRequest.ReceiverId = request.dto.AdminId;

        if (status == RequestStatus.confirmed.ToDisplay())
        {
            var wallet = await db.Wallets.FirstOrDefaultAsync(x => x.UserId == walletIncreaseRequest.SenderId, cancellationToken);
            if (wallet is null)
                throw new NotFoundException();

            WalletTransaction transaction = new()
            {
                WalletId = wallet.Id,
                CreatedAt = DateTime.Now,
                Amount = walletIncreaseRequest.Amount,
                BalanceBefore = wallet.Balance,
                BalanceAfter = wallet.Balance + walletIncreaseRequest.Amount,
                Reason = WalletTransactionReason.increaseByRequest.ToDisplay(),
                WalletIncreaseRequestId = walletIncreaseRequest.Id,
                AdminId = request.dto.AdminId
            };

            await db.WalletTransactions.AddAsync(transaction, cancellationToken);

            wallet.Balance += walletIncreaseRequest.Amount;

            walletIncreaseRequest.WalletTransaction = transaction;
        }

        await db.SaveChangesAsync(cancellationToken);
    }
}
