namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CompetitionMaxCompetitorsChangeNotAllowedException : Exception
{
    public CompetitionMaxCompetitorsChangeNotAllowedException() { }

    public CompetitionMaxCompetitorsChangeNotAllowedException(string message) : base(message) { }

    public CompetitionMaxCompetitorsChangeNotAllowedException(string message, Exception inner) : base(message, inner) { }
}
