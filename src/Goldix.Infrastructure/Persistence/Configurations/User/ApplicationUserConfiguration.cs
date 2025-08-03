using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Goldix.Infrastructure.Persistence.Configurations.User;

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

        builder.Property(x => x.Status)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired()
            .HasDefaultValue(UserStatus.waiting.ToDisplay());

        builder.Property(x => x.ImageUrl)
            .IsRequired(false);

        builder.Property(x => x.GroupId)
            .IsRequired(false);
    }
}