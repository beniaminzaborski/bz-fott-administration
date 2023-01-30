using Bz.Fott.Administration.Application.Common;
using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Application.Services;

public interface ICompetitionService : IApplicationService
{
    Task<CompetitionId> CreateCompetitionAsync();
}
