using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.OfferStatuses.Queries.GetOfferStatus;

public class GetPaginatedOfferStatusQuery : IRequest<PaginatedResult<OfferDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedOfferStatusQueryHandler : IRequestHandler<GetPaginatedOfferStatusQuery, PaginatedResult<OfferDto>>
{
    private readonly IOfferRepository _repository;
    private readonly IMapper _mapper;

    public GetPaginatedOfferStatusQueryHandler(IOfferRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<OfferDto>> Handle(GetPaginatedOfferStatusQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll()
            .OrderByDescending(e => e.Id)
            .ProjectTo<OfferDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<OfferDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}