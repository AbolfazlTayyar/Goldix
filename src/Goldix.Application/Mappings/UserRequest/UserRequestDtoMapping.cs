using Goldix.Application.Extensions;
using Goldix.Application.Models.UserRequest;

namespace Goldix.Application.Mappings.UserRequest;

public class UserRequestDtoMapping : Profile
{
    public UserRequestDtoMapping()
    {
        CreateMap<UserRequestDto, Domain.Entities.UserRequest.UserRequest>()
            .ReverseMap()
            .ForMember(d => d.UserName, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.ToShamsiDate()))
            .ForMember(d => d.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice.ToString("N0")))
            .ForMember(d => d.ProductTotalPrice, opt => opt.MapFrom(x => x.ProductTotalPrice.ToString("N0")));

    }
}
