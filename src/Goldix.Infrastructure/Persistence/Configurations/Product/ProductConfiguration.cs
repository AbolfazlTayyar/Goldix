namespace Goldix.Infrastructure.Persistence.Configurations.Product;

using Goldix.Domain.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product = Domain.Entities.Product.Product;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", schema: "Product");

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.BuyPrice)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(x => x.SellPrice)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(x => x.LastModifiedDate)
            .IsRequired(false);

        builder.Property(x => x.TradingStartTime)
            .IsRequired(false);

        builder.Property(x => x.TradingEndTime)
            .IsRequired(false);

        builder.Property(x => x.Comment)
            .IsRequired(false);

        builder.Property(x => x.MeasurementUnitId)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);
    }
}
