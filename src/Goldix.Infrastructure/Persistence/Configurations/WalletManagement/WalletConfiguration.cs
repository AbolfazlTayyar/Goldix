using Goldix.Domain.Constants;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Infrastructure.Persistence.Configurations.WalletManagement;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets", schema: "Wallet");

        builder.Property(x => x.UserId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.Balance)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
