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

        builder.Property(x => x.ReceiverId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.UserNotifications)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
