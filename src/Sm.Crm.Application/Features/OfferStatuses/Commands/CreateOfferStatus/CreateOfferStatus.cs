using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.OfferStatuses.Commands.CreateOfferStatus;

public class CreateOfferStatusCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CreateOfferStatusCommandHandler : IRequestHandler<CreateOfferStatusCommand, int>
{
    private readonly IRepository<OfferStatus, int> _offerStatusRepository;
    private readonly IMapper _mapper;

    public CreateOfferStatusCommandHandler(IRepository<OfferStatus, int> offerStatusRepository, IMapper mapper)
    {
        _offerStatusRepository = offerStatusRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOfferStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<OfferStatus>(request);
        var Id = await _offerStatusRepository.Create(entity);
        return Id;
    }
}