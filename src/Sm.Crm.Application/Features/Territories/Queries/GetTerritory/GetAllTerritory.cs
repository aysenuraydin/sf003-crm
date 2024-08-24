using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Territories.Queries.GetTerritory;
public record GetAllTerritoriesQuery : IRequest<ICollection<TerritoryDto>>;

public class GetAllTerritoryQueryHandler : IRequestHandler<GetAllTerritoriesQuery, ICollection<TerritoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTerritoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<TerritoryDto>> Handle(GetAllTerritoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.TerritoryRepository.GetAll().ToListAsync();
        return _mapper.Map<List<TerritoryDto>>(entities).ToList();
    }
}