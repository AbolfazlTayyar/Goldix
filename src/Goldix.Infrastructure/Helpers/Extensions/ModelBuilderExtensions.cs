using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Infrastructure.Helpers.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Set DeleteBehavior.Restrict by default for relations
    /// </summary>
    public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
    {
        IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (IMutableForeignKey fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    /// <summary>
    /// Configures custom table names and schema ("identity") for ASP.NET Identity entities.
    /// </summary>
    public static void ConfigureIdentityTables(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Identity");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Identity");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Identity");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Identity");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Identity");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Identity");
    }

    /// <summary>
    /// Ignores specific tables in the model.
    /// </summary>
    public static void IgnoreTablesAndForceTPT(this ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<BaseRequest>();

        modelBuilder.Entity<TradeRequest>()
            .ToTable("TradeRequests", schema: "Trade");

        modelBuilder.Entity<WalletIncreaseRequest>()
            .ToTable("WalletIncreaseRequests", schema: "Wallet");
    }
}
