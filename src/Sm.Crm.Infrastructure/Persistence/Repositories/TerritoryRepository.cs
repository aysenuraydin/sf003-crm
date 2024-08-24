using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;
public class TerritoryRepository : BaseRepository<Territory>, ITerritoryRepository
{
    public TerritoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}