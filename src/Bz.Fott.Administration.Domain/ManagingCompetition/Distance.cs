namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public record Distance
{
    public Distance(decimal amount, DistanceUnit unit)
    {
        if (amount < 0) throw new ArgumentException("Amount of distance must be greater than or equals to 0", nameof(amount));

        Amount = amount;
        Unit = unit;
    }

    public decimal Amount { get; init; }
    public DistanceUnit Unit { get; init; }
}
