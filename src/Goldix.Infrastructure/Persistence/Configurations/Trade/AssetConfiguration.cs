using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Infrastructure.Persistence.Configurations.Trade;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets", schema: "Trade");

        builder.Property(x => x.UserId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Count)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Assets)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Assets)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
