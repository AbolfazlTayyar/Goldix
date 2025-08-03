using Goldix.Application.Commands.Setting;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Setting;

public class UpdateSettingsCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<UpdateSettingsCommand>
{
    public async Task Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await db.ApplicationSettings.FirstOrDefaultAsync(cancellationToken);
        if (settings == null)
            throw new NotFoundException();

        settings.SmsApiKey = request.dto.SmsApiKey;

        await db.SaveChangesAsync(cancellationToken);
    }
}
