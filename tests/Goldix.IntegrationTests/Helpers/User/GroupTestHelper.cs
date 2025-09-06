using Goldix.Application.Commands.Group;
using Goldix.Application.Models.Group;
using Goldix.Infrastructure.Persistence;

namespace Goldix.UnitTests.Helpers.User;

public static class GroupTestHelper
{
    public static async Task SeedGroupAsync(ApplicationDbContext db, Domain.Entities.User.Group group)
    {
        db.Groups.Add(group);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }

    public static async Task SeedGroupsRandomlyAsync(ApplicationDbContext db, int count)
    {
        var groups = Enumerable.Range(1, count)
            .Select(i => new Domain.Entities.User.Group
            {
                Id = i,
                Name = $"Group {i}",
                BuyPriceDifferencePercent = i * 10,
                SellPriceDifferencePercent = i * 5,
                CreatedAt = DateTime.UtcNow.AddDays(-i)
            })
            .ToList();

        await db.Groups.AddRangeAsync(groups);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }

    public static ModifyGroupMembersCommand BuildModifyGroupMembersCommand()
    {
        return new ModifyGroupMembersCommand(1, new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string> { "1234567890" },
            UsersToRemove = new List<string>()
        });
    }
}
