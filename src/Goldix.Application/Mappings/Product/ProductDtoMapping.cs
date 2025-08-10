using Goldix.Application.Extensions;
using Goldix.Application.Models.Product;

namespace Goldix.Application.Mappings.Product;

public class ProductDtoMapping : Profile
{
    public ProductDtoMapping()
    {
        CreateMap<ProductDto, Domain.Entities.Product.Product>()
            .ReverseMap()
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.ToShamsiDate()))
            .ForMember(d => d.LastModifiedAt, opt => opt.MapFrom(x => x.LastModifiedAt.HasValue ? x.LastModifiedAt.Value.ToShamsiDate() : null));
    }
}
