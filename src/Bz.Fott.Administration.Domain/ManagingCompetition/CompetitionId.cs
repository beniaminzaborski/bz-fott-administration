using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CompetitionId : EntityId<Guid>
{
    public CompetitionId(Guid value) : base(value) { }

    public static CompetitionId From(Guid value)
    { 
        return new CompetitionId(value);
    }
}
