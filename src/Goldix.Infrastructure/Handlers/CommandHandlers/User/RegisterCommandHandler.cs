using Goldix.Application.Commands.User;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.User.Register;
using Goldix.Domain.Constants;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.User;

public class RegisterCommandHandler(IUserService userService, ApplicationDbContext db) : IRequestHandler<RegisterCommand, RegisterResponseDto>
{
    public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.RegisterUserAsync(request.dto.PhoneNumber, request.dto.FirstName, request.dto.LastName,
                                                                    request.dto.Password, RoleConstants.USER, cancellationToken);

        //wallet creation
        Wallet wallet = new()
        {
            UserId = user.Id,
            CreatedAt = DateTime.Now,
        };

        await db.Wallets.AddAsync(wallet, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return new RegisterResponseDto { UserId = user.Id };
    }
}
