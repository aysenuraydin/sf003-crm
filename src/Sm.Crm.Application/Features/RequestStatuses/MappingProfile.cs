using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Commands.UpdateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Queries;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.RequestStatuses;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<RequestStatus, CreateRequestStatusCommand>().ReverseMap();
        CreateMap<RequestStatus, UpdateRequestStatusCommand>().ReverseMap();
        CreateMap<RequestStatus, RequestStatusDto>();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}