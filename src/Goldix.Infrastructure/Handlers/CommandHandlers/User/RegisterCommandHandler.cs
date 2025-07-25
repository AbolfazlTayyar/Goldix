using Goldix.Application.Commands.User;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.Identity.Register;
using Goldix.Domain.Constants;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.User;

public class RegisterCommandHandler(IUserService userService) : IRequestHandler<RegisterCommand, RegisterResponsetDto>
{
    public async Task<RegisterResponsetDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.RegisterUserAsync(request.dto.PhoneNumber, request.dto.FirstName, request.dto.LastName,
                                                                    request.dto.Password, RoleConstants.USER, cancellationToken);

        return new RegisterResponsetDto { UserId = user.Id };
    }
}
