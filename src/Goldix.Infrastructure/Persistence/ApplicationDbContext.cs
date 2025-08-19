using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.Product;
using Goldix.Domain.Entities.Setting;
using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Infrastructure.Helpers.Extensions;

namespace Goldix.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<NotificationContent> NotificationContents { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    public DbSet<TradeRequest> TradeRequests { get; set; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<WalletIncreaseRequest> WalletIncreaseRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.ConfigureIdentityTables();
        modelBuilder.IgnoreTables();
    }

    public override int SaveChanges()
    {
        _cleanString();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _cleanString();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        _cleanString();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void _cleanString()
    {
        var changedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null);

                if (val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val)
                        continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }
}