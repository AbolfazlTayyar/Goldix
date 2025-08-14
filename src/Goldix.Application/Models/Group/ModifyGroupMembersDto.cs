namespace Goldix.Application.Models.Group;

public class ModifyGroupMembersDto
{
    public List<string> UsersToAdd { get; set; }
    public List<string> UsersToRemove { get; set; }
}
