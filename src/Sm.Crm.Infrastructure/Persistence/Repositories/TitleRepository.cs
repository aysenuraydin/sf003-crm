using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class TitleRepository : BaseRepository<Title, int>, ITitleRepository
{
    private readonly ApplicationDbContext _context;

    public TitleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Title>?> GetAll(int page = 1, int pageCount = 10)
    {
        var entities = await _context.Titles
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageCount)
            .Take(pageCount)
            .ToListAsync();
        return entities;
    }
}