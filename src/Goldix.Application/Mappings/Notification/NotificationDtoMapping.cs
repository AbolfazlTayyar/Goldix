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

        CreateMap<NotificationDto, UserNotification>()
            .ReverseMap()
            .ForMember(d => d.Title, opt => opt.MapFrom(x => x.NotificationContent.Title))
            .ForMember(d => d.Description, opt => opt.MapFrom(x => x.NotificationContent.Description))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(x => x.NotificationContent.CreatedAt.ToShamsiDate()));
    }
}
