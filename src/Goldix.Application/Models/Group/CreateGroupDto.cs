using Goldix.Application.Mappings.Common;

namespace Goldix.Application.Models.Group;

public class CreateUpdateGroupDto : IMapFrom<Domain.Entities.User.Group>
{
    public string Name { get; set; }
    public string BuyPriceDifferencePercent { get; set; }
    public string SellPriceDifferencePercent { get; set; }
}
