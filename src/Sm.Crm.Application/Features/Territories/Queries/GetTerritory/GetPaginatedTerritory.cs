using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Territories.Queries.GetTerritory;

public class GetPaginatedTerritoryQuery : IRequest<PaginatedResult<TerritoryDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedTerritoryQueryHandler : IRequestHandler<GetPaginatedTerritoryQuery, PaginatedResult<TerritoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedTerritoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<TerritoryDto>> Handle(GetPaginatedTerritoryQuery request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.TerritoryRepository.GetAll()
            .OrderByDescending(e => e.RegionId)
            .ProjectTo<TerritoryDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<TerritoryDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}