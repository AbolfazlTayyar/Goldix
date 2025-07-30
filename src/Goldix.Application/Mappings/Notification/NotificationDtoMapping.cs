using Goldix.Application.Extensions;
using Goldix.Application.Models.Notification;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Application.Mappings.Notification;

public class NotificationDtoMapping : Profile
{
    public NotificationDtoMapping()
    {
        CreateMap<NotificationDto, NotificationContent>()
            .ReverseMap()
            .ForMember(d => d.SenderName, opt => opt.MapFrom(x => $"{x.Sender.FirstName} {x.Sender.LastName}"))
            .AfterMap((src, dest) => dest.CreatedAt = src.CreatedAt.ToShamsiDate());
    }
}
