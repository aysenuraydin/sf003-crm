using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.OfferStatuses.Commands.UpdateOfferStatus;

public class UpdateOfferStatusCommand : IRequest<bool>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateOfferStatusCommandHandler : IRequestHandler<UpdateOfferStatusCommand, bool>
{
    private readonly IRepository<OfferStatus, int> _offerStatusRepository;
    private readonly IMapper _mapper;

    public UpdateOfferStatusCommandHandler(IRepository<OfferStatus, int> offerStatusRepository, IMapper mapper)
    {
        _offerStatusRepository = offerStatusRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateOfferStatusCommand request, CancellationToken cancellationTofen)
    {
        var entity = _mapper.Map<OfferStatus>(request);
        bool isSuccess = await _offerStatusRepository.Update(entity);
        return isSuccess;
    }
}