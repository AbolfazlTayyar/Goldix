using Goldix.Application.Models.Trade;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Application.Mappings.Trade;

public class AssetDtoMapping : Profile
{
    public AssetDtoMapping()
    {
        CreateMap<AssetDto, Asset>()
           .ReverseMap()
           .ForMember(d => d.ProductName, opt => opt.MapFrom(x => x.Product.Name));
    }
}
