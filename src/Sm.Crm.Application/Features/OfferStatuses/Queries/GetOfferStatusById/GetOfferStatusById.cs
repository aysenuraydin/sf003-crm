using AutoMapper;
using MediatR;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.OfferStatuses.Queries.GetOfferStatusById;

public class GetOfferStatusByIdQuery : IRequest<OfferDto?>
{
    public int Id { get; set; }

    public GetOfferStatusByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetOfferStatusByIdQueryHandler : IRequestHandler<GetOfferStatusByIdQuery, OfferDto?>
{
    private readonly IOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetOfferStatusByIdQueryHandler(IOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OfferDto?> Handle(GetOfferStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        return _mapper.Map<OfferDto>(entity);
    }
}