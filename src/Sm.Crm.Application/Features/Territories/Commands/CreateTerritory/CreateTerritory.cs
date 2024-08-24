using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.Territories.Commands.CreateTerritory;

public class CreateTerritoryCommand : IRequest<int>
{
    public int RegionId { get; set; }
    public string Name { get; set; }
}

public class CreateTerritoryCommandHandler : IRequestHandler<CreateTerritoryCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTerritoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTerritoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Territory>(request);
        await _unitOfWork.TerritoryRepository.Create(entity);
        return entity.Id;
    }
}