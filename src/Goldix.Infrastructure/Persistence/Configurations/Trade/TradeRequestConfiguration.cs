using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Infrastructure.Persistence.Configurations.Trade;

public class TradeRequestConfiguration : IEntityTypeConfiguration<TradeRequest>
{
    public void Configure(EntityTypeBuilder<TradeRequest> builder)
    {
        builder.ToTable("TradeRequests", schema: "Trade");

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

        builder.Property(x => x.Type)
            .HasMaxLength(DataSchemaConstants.DEFAULT_ENUM_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SentTradeRequests)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.ReceivedTradeRequests)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.WalletTransaction)
            .WithOne(x => x.TradeRequest)
            .HasForeignKey<TradeRequest>(x => x.WalletTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
