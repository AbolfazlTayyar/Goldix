using Goldix.Domain.Constants;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Infrastructure.Persistence.Configurations.WalletManagement;

public class WalletIncreaseRequestConfiguration : IEntityTypeConfiguration<WalletIncreaseRequest>
{
    public void Configure(EntityTypeBuilder<WalletIncreaseRequest> builder)
    {
        builder.ToTable("WalletIncreaseRequests", schema: "Wallet");

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.SenderId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.ReceiverId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired(false);

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SentWalletIncreaseRequests)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.ReceivedWalletIncreaseRequests)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.WalletTransaction)
            .WithOne(x => x.WalletIncreaseRequest)
            .HasForeignKey<WalletIncreaseRequest>(x => x.WalletTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
