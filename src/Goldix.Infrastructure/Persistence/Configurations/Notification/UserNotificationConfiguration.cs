using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Infrastructure.Persistence.Configurations.Notification;

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
    public void Configure(EntityTypeBuilder<UserNotification> builder)
    {
        builder.ToTable("UserNotifications", schema: "Notification");

        builder.Property(x => x.NotificationContentId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();
    }
}
