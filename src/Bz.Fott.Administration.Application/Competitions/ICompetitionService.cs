using Bz.Fott.Administration.Application.Common;
using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Application.Competitions;

public interface ICompetitionService : IApplicationService
{
    Task<Guid> CreateCompetitionAsync(CreateCompetitionDto dto);
    
    Task<CompetitionDto> GetCompetitionAsync(Guid id);

    Task OpenRegistrationAsync(Guid id);

    Task CompleteRegistrationAsync(Guid id);

    Task ChangeMaxCompetitors(Guid id, int maxCompetitors);

    Task AddCheckpoint(Guid competitionId, AddCheckpointRequestDto checkpointDto);

    Task RemoveCheckpoint(Guid competitionId, Guid checkpointId);
}
