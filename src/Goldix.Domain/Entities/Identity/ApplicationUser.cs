using Goldix.Domain.Entities.Notification;

namespace Goldix.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime CreateDate { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    #region Navigation Properties
    public List<NotificationContent> NotificationContents { get; set; }
    #endregion
}
