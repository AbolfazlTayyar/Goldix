using Goldix.Domain.Entities.Common;

namespace Goldix.Domain.Entities.Setting;

public class ApplicationSetting : BaseEntity
{
    public string SmsApiKey { get; set; }
}
