using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Queries.GetTitleById;

public class GetTitleByIdQuery : IRequest<TitleDto?>
{
    public int Id { get; set; }

    public GetTitleByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTitleByIdQueryHandler : IRequestHandler<GetTitleByIdQuery, TitleDto?>
{
    private readonly ITitleRepository _titleRepository;
    private readonly IMapper _mapper;

    public GetTitleByIdQueryHandler(ITitleRepository titleRepository, IMapper mapper)
    {
        _titleRepository = titleRepository;
        _mapper = mapper;
    }

    public async Task<TitleDto?> Handle(GetTitleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _titleRepository.GetById(request.Id);
        return _mapper.Map<TitleDto>(entity);
    }
}