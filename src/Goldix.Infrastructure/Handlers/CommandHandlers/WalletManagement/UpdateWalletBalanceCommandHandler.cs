using Goldix.Application.Commands.WalletManagement;
using Goldix.Application.Exceptions;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Domain.Enums.WalletManagement;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.WalletManagement;

public class UpdateWalletBalanceCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateWalletBalanceCommand>
{
    public async Task Handle(UpdateWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var wallet = await db.Wallets.FirstOrDefaultAsync(x => x.UserId == request.id, cancellationToken);
        if (wallet is null)
            throw new NotFoundException();

        var newBalanceDecimal = decimal.Parse(request.dto.NewBalance);
        if (newBalanceDecimal == wallet.Balance)
            throw new BadRequestException();

        var transactionReason = wallet.Balance > newBalanceDecimal ?
            WalletTransactionReason.decreaseByAdmin :
            WalletTransactionReason.increaseByAdmin;

        WalletTransaction walletTransaction = new()
        {
            WalletId = wallet.Id,
            CreatedAt = DateTime.Now,
            Amount = newBalanceDecimal,
            BalanceBefore = wallet.Balance,
            BalanceAfter = newBalanceDecimal,
            Reason = transactionReason.ToDisplay(),
            AdminId = request.dto.AdminId
        };

        await db.WalletTransactions.AddAsync(walletTransaction, cancellationToken);

        wallet.Balance = newBalanceDecimal;
        wallet.UpdatedAt = DateTime.Now;

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
