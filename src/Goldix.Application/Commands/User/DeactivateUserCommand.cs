namespace Goldix.Application.Commands.User;

public record DeactivateUserCommand(string id) : IRequest;
