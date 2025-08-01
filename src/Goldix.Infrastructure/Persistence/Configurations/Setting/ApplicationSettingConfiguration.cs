using Goldix.Domain.Entities.Setting;

namespace Goldix.Infrastructure.Persistence.Configurations.Setting;

public class ApplicationSettingConfiguration : IEntityTypeConfiguration<ApplicationSetting>
{
    public void Configure(EntityTypeBuilder<ApplicationSetting> builder)
    {
        builder.ToTable("ApplicationSettings", schema: "Setting");
    }
}
