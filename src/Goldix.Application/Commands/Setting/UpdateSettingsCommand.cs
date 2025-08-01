using Goldix.Application.Models.Setting;

namespace Goldix.Application.Commands.Setting;

public record UpdateSettingsCommand(CreateUpdateSettingsDto dto) : IRequest;
