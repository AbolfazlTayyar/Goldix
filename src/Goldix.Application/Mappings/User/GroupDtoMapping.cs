using Goldix.Application.Extensions;
using Goldix.Application.Models.Group;

namespace Goldix.Application.Mappings.User;

public class GroupDtoMapping : Profile
{
    public GroupDtoMapping()
    {
        CreateMap<GroupDto, Domain.Entities.User.Group>()
            .ReverseMap()
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.ToShamsiDate()));
    }
}
