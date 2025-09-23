using Goldix.Application.Commands.Setting;
using Goldix.Application.Exceptions;
using Goldix.Domain.Entities.Setting;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Setting;

public class CreateSettingsCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateSettingsCommand>
{
    public async Task Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
    {
        if (await db.ApplicationSettings.AnyAsync(cancellationToken))
            throw new BadRequestException();

        var settings = mapper.Map<ApplicationSetting>(request.dto);
        await db.ApplicationSettings.AddAsync(settings, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
