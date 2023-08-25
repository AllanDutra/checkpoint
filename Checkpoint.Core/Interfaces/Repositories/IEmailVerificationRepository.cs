using Checkpoint.Core.Entities;

namespace Checkpoint.Core.Interfaces.Repositories
{
#nullable enable
    public interface IEmailVerificationRepository
    {
        Task<EmailVerification?> GetByEmployeeEmail(string employeeEmail);

        Task RegisterAsync(EmailVerification emailVerification);

        Task SaveChangesAsync();
    }
}
