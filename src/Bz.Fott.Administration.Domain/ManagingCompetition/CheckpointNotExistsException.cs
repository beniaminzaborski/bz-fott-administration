namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CheckpointNotExistsException : Exception
{
    public CheckpointNotExistsException() { }

    public CheckpointNotExistsException(string message) : base(message) { }

    public CheckpointNotExistsException(string message, Exception inner) : base(message, inner) { }
}
