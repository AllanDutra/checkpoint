using Checkpoint.Core.Interfaces.Repositories;

namespace Checkpoint.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEmailVerificationRepository EmailVerifications { get; }
        IEmployeeRepository Employees { get; }
        IPointLogRepository PointLogs { get; }
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
