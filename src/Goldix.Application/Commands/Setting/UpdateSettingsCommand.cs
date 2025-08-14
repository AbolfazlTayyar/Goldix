using Goldix.Application.Models.Setting;

namespace Goldix.Application.Commands.Setting;

public record UpdateSettingsCommand(SettingsDto dto) : IRequest;
