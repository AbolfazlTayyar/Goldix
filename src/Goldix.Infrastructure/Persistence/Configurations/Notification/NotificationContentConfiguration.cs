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
            .HasMaxLength(DataSchemaConstants.DEFAULT_STRING_LENGTH)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.NotificationContents)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.UserNotifications)
            .WithOne(x => x.NotificationContent)
            .HasForeignKey(x => x.NotificationContentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
