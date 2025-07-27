using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Infrastructure.Persistence.Configurations.Notification;

public class NotificationContentConfiguration : IEntityTypeConfiguration<NotificationContent>
{
    public void Configure(EntityTypeBuilder<NotificationContent> builder)
    {
        builder.ToTable("NotificationContents", schema: "Notification");

        builder.Property(x => x.SenderId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired(false)
            .HasMaxLength(DataSchemaConstants.DEFAULT_STRING_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
