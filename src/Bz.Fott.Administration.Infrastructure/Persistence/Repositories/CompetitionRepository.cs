using Bz.Fott.Administration.Domain.ManagingCompetition;
using Bz.Fott.Administration.Infrastructure.Persistence.Common;

namespace Bz.Fott.Administration.Infrastructure.Persistence.Repositories;

internal class CompetitionRepository : Repository<Competition, CompetitionId, ApplicationDbContext>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
