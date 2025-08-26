using Goldix.Application.Commands.Trade;
using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Trade;

public class CreateTradeRequestCommandHandler(ApplicationDbContext db, IUserService userService) : IRequestHandler<CreateTradeRequestCommand>
{
    public async Task Handle(CreateTradeRequestCommand request, CancellationToken cancellationToken)
    {
        if (!decimal.TryParse(request.dto.ProductPrice, out var productPrice))
            throw new BadRequestException();

        var userId = userService.GetCurrentUserId();
        var user = await db.Users
            .Include(x => x.Wallet)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
            throw new NotFoundException();

        var totalPrice = productPrice * request.dto.ProductCount;
        if (user.Wallet.Balance < totalPrice)
            throw new BadRequestException();

        TradeRequest tradeRequest = new()
        {
            ProductId = request.dto.ProductId,
            ProductCount = request.dto.ProductCount,
            ProductPrice = productPrice,
            ProductTotalPrice = totalPrice,
            Type = request.dto.Type,
            SentAt = DateTime.Now,
            Status = RequestStatus.pending.ToDisplay(),
            SenderId = userId
        };

        await db.TradeRequests.AddAsync(tradeRequest, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
