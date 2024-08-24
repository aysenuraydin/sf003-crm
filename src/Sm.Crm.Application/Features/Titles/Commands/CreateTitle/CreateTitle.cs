using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Events;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Commands.CreateTitle;

public class CreateTitleCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, int>
{
    private readonly ITitleRepository _titleRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateTitleCommandHandler(ITitleRepository titleRepository, IMapper mapper, IPublisher publisher)
    {
        _titleRepository = titleRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<int> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Title>(request);
        var id = await _titleRepository.Create(entity);

        entity.AddDomainEvent(new TitleCreatedEvent(entity));

        return id;
    }
}