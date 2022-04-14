using System.Threading.Tasks;

namespace Framework.Core
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();

        Task AddOutboxMessage(OutboxMessage outboxMessage);
    }
}
