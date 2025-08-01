using Goldix.Application.Mappings.Common;
using Goldix.Domain.Entities.Setting;

namespace Goldix.Application.Models.Setting;

public class CreateUpdateSettingsDto : IMapFrom<ApplicationSetting>
{
    public string SmsApiKey { get; set; }
}
