using Goldix.Application.Models.Identity.Register;

namespace Goldix.Application.Commands.User;

public record RegisterCommand(RegisterRequestDto dto) : IRequest<RegisterResponsetDto>;
