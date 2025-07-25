using Goldix.Application.Interfaces.Repositories.Common;
using Goldix.Application.Interfaces.Repositories.Identity;
using Goldix.Infrastructure.Persistence;
using Goldix.Infrastructure.Services.Identity;

namespace Goldix.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        UserRepository = new UserRepository(db);
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
