using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Territories.Queries.GetTerritoryById;

public record GetTerritoryByIdQuery(int Id) : IRequest<TerritoryDto?>;

public class GetTerritoryByIdQueryHandler : IRequestHandler<GetTerritoryByIdQuery, TerritoryDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTerritoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TerritoryDto?> Handle(GetTerritoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TerritoryRepository.GetAll().FirstOrDefaultAsync(e => e.RegionId == request.Id);
        return _mapper.Map<TerritoryDto>(entity);
    }
}