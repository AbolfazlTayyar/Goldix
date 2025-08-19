using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Common;

namespace Goldix.Infrastructure.Persistence.Configurations.Common;

public class RequestConfiguration : IEntityTypeConfiguration<BaseRequest>
{
    public void Configure(EntityTypeBuilder<BaseRequest> builder)
    {
        builder.Property(x => x.SentAt)
            .IsRequired();

        builder.Property(x => x.RespondedAt)
            .IsRequired(false);

        builder.Property(x => x.Status)
            .HasMaxLength(DataSchemaConstants.DEFAULT_ENUM_LENGTH)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.WalletTransactionId)
            .IsRequired(false);

        builder.Property(x => x.SenderId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.ReceiverId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired(false);
    }
}
