namespace Goldix.Application.Models.User;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string CreatedAt { get; set; }
    public bool IsOnline { get; set; }
    public string WalletBalance { get; set; }
    public string GroupName { get; set; }
}
