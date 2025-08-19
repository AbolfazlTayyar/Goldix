using Goldix.Application.Commands.Trade;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.UserRequest;

public class ModifyUserRequestCommandHandler(ApplicationDbContext db) : IRequestHandler<ModifyTradeRequestCommand>
{
    public async Task Handle(ModifyTradeRequestCommand request, CancellationToken cancellationToken)
    {
        var record = await db.TradeRequests.FirstOrDefaultAsync(x=>x.Id == request.id, cancellationToken);
        if (record is null)
            throw new NotFoundException();

        record.Status = request.dto.Status.ToDisplay();
        await db.SaveChangesAsync(cancellationToken);
    }
}
