using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Commands.DeleteTitle;

public record DeleteTitleCommand(int Id) : IRequest<bool>;

public class DeleteTitleCommandHandler : IRequestHandler<DeleteTitleCommand, bool>
{
    private readonly ITitleRepository _titleRepository;
    private readonly IMapper _mapper;

    public DeleteTitleCommandHandler(ITitleRepository titleRepository, IMapper mapper)
    {
        _titleRepository = titleRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteTitleCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _titleRepository.DeleteById(request.Id);
        return isSuccess;
    }
}