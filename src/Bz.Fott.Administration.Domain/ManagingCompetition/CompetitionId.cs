using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CompetitionId : EntityId<Guid>
{
    public CompetitionId(Guid id) : base(id) { }

    public static CompetitionId From(Guid id)
    { 
        return new CompetitionId(id);
    }
}
