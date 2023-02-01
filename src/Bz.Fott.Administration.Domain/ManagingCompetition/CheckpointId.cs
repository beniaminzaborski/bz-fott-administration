using Bz.Fott.Administration.Domain.Common;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record CheckpointId : EntityId<Guid>
{
    public CheckpointId(Guid value) : base(value) { }

    public static CheckpointId From(Guid value)
    {
        return new CheckpointId(value);
    }
}
