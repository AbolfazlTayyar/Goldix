using Goldix.Application.Models.User.Register;

namespace Goldix.Application.Commands.User;

public record RegisterCommand(RegisterRequestDto dto) : IRequest<RegisterResponsetDto>;
