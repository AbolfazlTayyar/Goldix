using Goldix.Application.Interfaces.Repositories.Identity;

namespace Goldix.Application.Interfaces.Repositories.Common;

public interface IUnitOfWork : IDisposable
{
    Task CompleteAsync(CancellationToken cancellationToken);

    IUserRepository UserRepository { get; }
}
