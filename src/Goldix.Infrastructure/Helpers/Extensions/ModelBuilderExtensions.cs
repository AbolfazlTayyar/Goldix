using Goldix.Domain.Entities.User;

namespace Goldix.Infrastructure.Helpers.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Set DeleteBehavior.Restrict by default for relations
    /// </summary>
    /// <param name="modelBuilder"></param>
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
    /// <param name="modelBuilder"></param>
    public static void ConfigureIdentityTables(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("Users", "identity");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles", "identity");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "identity");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "identity");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "identity");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "identity");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "identity");
    }
}
