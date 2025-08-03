using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;

namespace Goldix.Infrastructure.Persistence.Configurations.User;

public class UserRequestConfiguration : IEntityTypeConfiguration<UserRequest>
{
    public void Configure(EntityTypeBuilder<UserRequest> builder)
    {
        builder.ToTable("UserRequests", schema: "Identity");

        builder.Property(x => x.UserId)
            .HasMaxLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductCount)
            .IsRequired();

        builder.Property(x => x.ProductPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ProductTotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasMaxLength(DataSchemaConstants.DEFAULT_ENUM_LENGTH)
            .IsRequired();
    }
}
