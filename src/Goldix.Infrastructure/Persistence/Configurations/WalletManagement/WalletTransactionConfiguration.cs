using Goldix.Domain.Constants;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Infrastructure.Persistence.Configurations.WalletManagement;

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.ToTable("WalletTransactions", schema: "Wallet");

        builder.Property(x => x.WalletId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BalanceBefore)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BalanceAfter)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(DataSchemaConstants.DEFAULT_ENUM_LENGTH)
            .IsRequired();
    }
}
