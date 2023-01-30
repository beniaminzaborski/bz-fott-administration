namespace Bz.Fott.Administration.Domain.Common;

public abstract record EntityId<TId>
{
	public EntityId(TId id)
	{
		Id = id;
	}

    public TId Id { get; init; }
}
