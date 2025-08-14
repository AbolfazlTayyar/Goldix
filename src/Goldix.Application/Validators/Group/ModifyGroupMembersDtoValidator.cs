using Goldix.Application.Models.Group;

namespace Goldix.Application.Validators.Group;

public class ModifyGroupMembersDtoValidator : AbstractValidator<ModifyGroupMembersDto>
{
    public ModifyGroupMembersDtoValidator()
    {
        RuleFor(x => x)
             .Must(HaveAtLeastOneOperation)
             .WithMessage("کاربری انتخاب نشده است.");
    }

    private bool HaveAtLeastOneOperation(ModifyGroupMembersDto dto) => 
        (dto.UsersToAdd?.Any() == true) || (dto.UsersToRemove?.Any() == true);
}
