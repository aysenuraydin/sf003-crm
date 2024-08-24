using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.Territories.Commands.UptadeTerritory;

public class UpdateTerritoryCommand : IRequest<bool>
{
    public int RegionId { get; set; }
    public string Name { get; set; }
}

public class UpdateTerritoryCommandHandler : IRequestHandler<UpdateTerritoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTerritoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTerritoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Territory>(request);
        bool isSuccess = await _unitOfWork.TerritoryRepository.Update(entity);
        return isSuccess;
    }
}