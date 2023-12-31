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

        public async Task<EmailVerification?> GetByEmployeeEmailAsync(string employeeEmail)
        {
            return await _dbContext.EmailVerifications.FirstOrDefaultAsync(
                ev => ev.EmployeeEmail == employeeEmail
            );
        }

        public async Task RegisterAsync(EmailVerification emailVerification)
        {
            await _dbContext.EmailVerifications.AddAsync(emailVerification);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public async Task DeleteByEmployeeEmailsAsync(List<string> employeeEmails)
        {
            var emailVerifications = await _dbContext.EmailVerifications
                .Where(ev => employeeEmails.Contains(ev.EmployeeEmail))
                .ToListAsync();

            if (emailVerifications == null)
                return;

            _dbContext.EmailVerifications.RemoveRange(emailVerifications);

            await _dbContext.SaveChangesAsync();
        }
    }
}
