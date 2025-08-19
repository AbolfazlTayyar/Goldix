using Goldix.Application.Extensions;
using Goldix.Application.Models.Trade;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Application.Mappings.Trade;

public class TradeRequestDtoMapping : Profile
{
    public TradeRequestDtoMapping()
    {
        CreateMap<TradeRequestDto, TradeRequest>()
            .ReverseMap()
            .ForMember(d => d.UserName, opt => opt.MapFrom(x => x.Sender.FirstName + " " + x.Sender.LastName))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(x => x.SentAt.ToShamsiDate()))
            .ForMember(d => d.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice.ToString("N0")))
            .ForMember(d => d.ProductTotalPrice, opt => opt.MapFrom(x => x.ProductTotalPrice.ToString("N0")));

    }
}
