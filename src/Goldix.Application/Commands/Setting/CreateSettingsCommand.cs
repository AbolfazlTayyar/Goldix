using Goldix.Application.Models.Setting;

namespace Goldix.Application.Commands.Setting;

public record CreateSettingsCommand(SettingsDto dto) : IRequest;
