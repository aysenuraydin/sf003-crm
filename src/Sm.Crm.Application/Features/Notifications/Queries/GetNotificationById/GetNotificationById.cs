﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Notifications.Queries.GetNotificationById;
public record GetNotificationByIdQuery(int Id) : IRequest<NotificationDto?>;

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetNotificationByIdQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<NotificationDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _notificationRepository.GetAll()
            .FirstOrDefaultAsync(e => e.Id.Equals(request.Id));

        return _mapper.Map<NotificationDto>(entity);
    }
}