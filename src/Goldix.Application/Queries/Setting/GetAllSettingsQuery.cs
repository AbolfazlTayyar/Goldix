using Goldix.Application.Models.Setting;

namespace Goldix.Application.Queries.Setting;

public record GetAllSettingsQuery : IRequest<List<SettingsDto>>;
