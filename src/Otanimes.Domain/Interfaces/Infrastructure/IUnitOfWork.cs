using System.Threading;
using System.Threading.Tasks;

namespace Otanimes.Domain.Interfaces.Infrastructure;

public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    int SaveChanges();
    
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}