namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CannotOpenRegistrationException : Exception
{
    public CannotOpenRegistrationException() { }

    public CannotOpenRegistrationException(string message) : base(message) { }

    public CannotOpenRegistrationException(string message, Exception inner) : base(message, inner) { }
}
