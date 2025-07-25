using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Persistence.Configurations.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Configure properties
        builder.Property(x => x.FirstName)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.CreateDate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);
    }
}