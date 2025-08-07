using Goldix.Application.Extensions;
using Goldix.Application.Models.Product;

namespace Goldix.Application.Mappings.Product;

public class ProductDtoMapping : Profile
{
    public ProductDtoMapping()
    {
        CreateMap<ProductDto, Domain.Entities.Product.Product>()
            .ReverseMap()
            .ForMember(d => d.CreateDate, opt => opt.MapFrom(x => x.CreateDate.ToShamsiDate()))
            .ForMember(d => d.LastModifiedDate, opt => opt.MapFrom(x => x.LastModifiedDate.HasValue ? x.LastModifiedDate.Value.ToShamsiDate() : null));
    }
}
