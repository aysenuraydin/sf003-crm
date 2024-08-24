using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;

public class UpdateTitleCommand : IRequest<bool>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateTitleCommandHandler : IRequestHandler<UpdateTitleCommand, bool>
{
    private readonly ITitleRepository _titleRepository;
    private readonly IMapper _mapper;

    public UpdateTitleCommandHandler(ITitleRepository titleRepository, IMapper mapper)
    {
        _titleRepository = titleRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Title>(request);
        bool isSuccess = await _titleRepository.Update(entity);
        return isSuccess;
    }
}