using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.OfferStatuses.Commands.DeleteOfferStatus;

public record DeleteOfferStatusCommand(int Id) : IRequest<bool>;

public class DeleteOfferStatusCommandHandler : IRequestHandler<DeleteOfferStatusCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IRepository<OfferStatus, int> _offerStatusRepository;

    public DeleteOfferStatusCommandHandler(IRepository<OfferStatus, int> offerStatusRepository, IMapper mapper)
    {
        _offerStatusRepository = offerStatusRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteOfferStatusCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _offerStatusRepository.DeleteById(request.Id);
        return isSuccess;
    }
}