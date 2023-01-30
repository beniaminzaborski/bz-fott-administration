namespace Bz.Fott.Administration.Application.Common;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}
