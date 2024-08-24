using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.RequestStatuses.Commands.DeleteRequestStatus;
public record DeleteRequestStatusCommand(int Id) : IRequest<bool>;

public class DeleteRequestStatusCommandHandler : IRequestHandler<DeleteRequestStatusCommand, bool>
{
    private readonly IRequestStatusRepository _repository;

    public DeleteRequestStatusCommandHandler(IRequestStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteRequestStatusCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _repository.DeleteById(request.Id);
        return isSuccess;
    }
}