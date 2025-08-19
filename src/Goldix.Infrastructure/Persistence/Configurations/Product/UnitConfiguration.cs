using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Product;

namespace Goldix.Infrastructure.Persistence.Configurations.Product;

public class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("MeasurementUnits", schema: "Product");

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_STRING_LENGTH)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.MeasurementUnit)
            .HasForeignKey(x => x.MeasurementUnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
