using AutoMapper;

using Sm.Crm.Application.Features.Notifications.Commands.CreateNotification;
using Sm.Crm.Application.Features.Notifications.Commands.UpdateNotification;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Notifications;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
        CreateMap<Notification, CreateNotificationCommand>().ReverseMap();
        CreateMap<Notification, UpdateNotificationCommand>().ReverseMap();
        CreateMap<Notification, Notifications.Queries.NotificationDto>()
			.ForMember(e => e.Title, m => m.MapFrom(u => u.Title != null ? u.Title : "-"))	
			.ReverseMap();
	}
}