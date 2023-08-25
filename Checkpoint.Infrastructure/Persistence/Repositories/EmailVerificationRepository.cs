using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint.Infrastructure.Persistence.Repositories
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly CheckpointDbContext _dbContext;

        public EmailVerificationRepository(CheckpointDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailVerification?> GetByEmployeeEmail(string employeeEmail)
        {
            return await _dbContext.EmailVerifications.FirstOrDefaultAsync(
                ev => ev.EmployeeEmail == employeeEmail
            );
        }

        public async Task RegisterAsync(EmailVerification emailVerification)
        {
            await _dbContext.AddAsync(emailVerification);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
