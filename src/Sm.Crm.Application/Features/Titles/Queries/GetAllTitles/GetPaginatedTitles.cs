using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;

namespace Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;

public class GetPaginatedTitlesQuery : IRequest<PaginatedResult<TitleDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedTitlesQueryHandler : IRequestHandler<GetPaginatedTitlesQuery, PaginatedResult<TitleDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetPaginatedTitlesQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<TitleDto>> Handle(GetPaginatedTitlesQuery request, CancellationToken cancellationToken)
    {
        var entities = _db.Titles
              .OrderByDescending(e => e.Id)
              .ProjectTo<TitleDto>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(request.Search))
        {
            entities = entities.Where(e => e.Name.Contains(request.Search));
        }

        return await PaginatedResult<TitleDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}