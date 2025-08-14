using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;

namespace Goldix.Infrastructure.Persistence.Configurations.User;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups", schema: "Identity");

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.BuyPriceDifferencePercent)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.SellPriceDifferencePercent)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
