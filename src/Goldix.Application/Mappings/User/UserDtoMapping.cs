using Goldix.Application.Extensions;
using Goldix.Application.Models.User;
using Goldix.Domain.Entities.User;

namespace Goldix.Application.Mappings.User;

public class UserDtoMapping : Profile
{
    public UserDtoMapping()
    {
        CreateMap<UserDto, ApplicationUser>()
            .ReverseMap()
            .ForMember(d => d.CreateDate, opt => opt.MapFrom(x => x.CreateDate.ToShamsiDate()));
    }
}
