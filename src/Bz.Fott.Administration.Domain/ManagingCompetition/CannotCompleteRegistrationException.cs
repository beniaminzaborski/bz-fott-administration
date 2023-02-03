namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CannotCompleteRegistrationException : Exception
{
	public CannotCompleteRegistrationException() { }

	public CannotCompleteRegistrationException(string message) : base(message) { }

	public CannotCompleteRegistrationException(string message, Exception inner) : base(message, inner) { }
}
