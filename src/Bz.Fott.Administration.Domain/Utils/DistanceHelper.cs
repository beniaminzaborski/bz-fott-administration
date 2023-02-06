using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Domain.Utils;

public static class DistanceHelper
{
    private static decimal marathonDistanceInKilometers = 42.195m;

    public static Distance Halfmarathon() => new Distance(Marathon().Amount / 2, DistanceUnit.Kilometers);

    public static Distance Marathon() => new Distance(marathonDistanceInKilometers, DistanceUnit.Kilometers);

    public static Distance From(decimal amount, string unit)
    {
        if (Enum.TryParse<DistanceUnit>(unit, out var distanceUnit))
        {
            switch (distanceUnit)
            {
                case DistanceUnit.Kilometers:
                    return FromKilometers(amount);
                case DistanceUnit.Meters:
                    return FromMeters(amount);
            }
        }

        return FromKilometers(amount);
    }

    public static Distance FromKilometers(decimal amount)
    {
        if (amount == marathonDistanceInKilometers)
            return Marathon();
        if (amount == Marathon().Amount / 2)
            return Halfmarathon();
        return new Distance(amount, DistanceUnit.Kilometers);
    }

    public static Distance FromMeters(decimal amount)
    {
        if (amount == marathonDistanceInKilometers * 1000)
            return Marathon();
        if (amount == Marathon().Amount * 1000 / 2)
            return Halfmarathon();
        return new Distance(amount, DistanceUnit.Meters);
    }
}
