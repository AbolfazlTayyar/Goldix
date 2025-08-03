using Goldix.Application.Commands.WalletManagement;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.WalletManagement;

public class UpdateWalletBalanceCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateWalletBalanceCommand>
{
    public async Task Handle(UpdateWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        var wallet = await db.Wallets.FirstOrDefaultAsync(x => x.UserId == request.id, cancellationToken);
        if (wallet is null)
            throw new NotFoundException();

        wallet.Balance = decimal.Parse(request.dto.NewBalance);
        wallet.UpdatedAt = DateTime.Now;
        await db.SaveChangesAsync(cancellationToken);
    }
}
