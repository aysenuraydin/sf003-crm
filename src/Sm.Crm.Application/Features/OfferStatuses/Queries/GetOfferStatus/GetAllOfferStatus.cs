using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.OfferStatuses.Queries.GetOfferStatus;

public class GetAllOfferStatusQuery : IRequest<ICollection<OfferDto>>;

public class GetAllOfferStatusQueryHandler : IRequestHandler<GetAllOfferStatusQuery, ICollection<OfferDto>>
{
    private readonly IOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOfferStatusQueryHandler(IOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<OfferDto>> Handle(GetAllOfferStatusQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToListAsync();
        return _mapper.Map<List<OfferDto>>(entities);
    }
}