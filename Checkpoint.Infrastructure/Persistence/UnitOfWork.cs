using Checkpoint.Core.Interfaces;
using Checkpoint.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Checkpoint.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;

        private readonly CheckpointDbContext _dbContext;

        public UnitOfWork(
            CheckpointDbContext dbContext,
            IEmailVerificationRepository emailVerifications,
            IEmployeeRepository employees,
            IPointLogRepository pointLogs
        )
        {
            _dbContext = dbContext;
            EmailVerifications = emailVerifications;
            Employees = employees;
            PointLogs = pointLogs;
        }

        public IEmailVerificationRepository EmailVerifications { get; }

        public IEmployeeRepository Employees { get; }

        public IPointLogRepository PointLogs { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _dbContext.Dispose();
        }
    }
}
