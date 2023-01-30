using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Domain.Utils;

public static class DistanceHelper
{
    private static decimal marathonDistanceInKilometers = 42.195m;

    public static Distance Halfmarathon() => new Distance(Marathon().Amount / 2, DistanceUnit.Kilometers);

    public static Distance Marathon() => new Distance(marathonDistanceInKilometers, DistanceUnit.Kilometers);
}
