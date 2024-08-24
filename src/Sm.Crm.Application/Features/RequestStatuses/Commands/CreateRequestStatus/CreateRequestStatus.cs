using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;

public class CreateRequestStatusCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateRequestStatusCommandHandler : IRequestHandler<CreateRequestStatusCommand, int>
{
    private readonly IRequestStatusRepository _requestStatusRepository;
    private readonly IMapper _mapper;

    public CreateRequestStatusCommandHandler(IRequestStatusRepository requestStatusRepository, IMapper mapper)
    {
        _requestStatusRepository = requestStatusRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateRequestStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<RequestStatus>(request);
        var id = await _requestStatusRepository.Create(entity);

        return id;
    }
}