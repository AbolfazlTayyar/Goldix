namespace Goldix.Application.Interfaces.Identity;

public interface IUserService
{
    Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
}
