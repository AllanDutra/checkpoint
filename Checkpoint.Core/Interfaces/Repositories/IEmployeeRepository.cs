using Checkpoint.Core.Entities;
using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task RegisterAsync(Employee employee);
        Task<bool> AlreadyAnEmployeeWithTheSameEmail(string email);
        Task<bool> AlreadyAnEmployeeWithTheSameUsername(string username);
        Task<EmployeeClaimsViewModel> GetEmployeeClaimsByUsernameAndPassword(
            string username,
            string encryptedPassword
        );
    }
}
