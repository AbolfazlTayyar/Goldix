using Goldix.Application.Mappings.Common;
using Goldix.Domain.Entities.Setting;

namespace Goldix.Application.Models.Setting;

public class SettingsDto : IMapFrom<ApplicationSetting>
{
    public int Id { get; set; }
    public string SmsApiKey { get; set; }
}
