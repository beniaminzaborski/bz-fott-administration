namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CompetitionPlaceCityInvalidException : Exception
{
    public CompetitionPlaceCityInvalidException() { }

    public CompetitionPlaceCityInvalidException(string message) : base(message) { }

    public CompetitionPlaceCityInvalidException(string message, Exception inner) : base(message, inner) { }
}
