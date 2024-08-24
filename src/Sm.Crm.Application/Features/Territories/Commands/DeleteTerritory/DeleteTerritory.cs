using MediatR;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Territories.Commands.DeleteTerritory;

public record DeleteTerritoryCommand(int Id) : IRequest<bool>;

public class DeleteTerritoryCommandHandler : IRequestHandler<DeleteTerritoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTerritoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTerritoryCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _unitOfWork.TerritoryRepository.DeleteById(request.Id);
        return isSuccess;
    }
}