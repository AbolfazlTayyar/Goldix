using Goldix.Application.Commands.Setting;
using Goldix.Domain.Entities.Setting;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Setting;

public class CreateSettingsCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateSettingsCommand>
{
    public async Task Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = mapper.Map<ApplicationSetting>(request.dto);
        await db.ApplicationSettings.AddAsync(settings, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
