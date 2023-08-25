using Checkpoint.Core.Entities;
using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Core.Interfaces.Repositories
{
#nullable enable
    public interface IEmployeeRepository
    {
        Task RegisterAsync(Employee employee);
        Task<bool> AlreadyAnEmployeeWithTheSameEmailAsync(string email);
        Task<bool> AlreadyAnEmployeeWithTheSameUsernameAsync(string username);
        Task<EmployeeClaimsViewModel?> GetEmployeeClaimsByUsernameAndPasswordAsync(
            string username,
            string encryptedPassword
        );
        Task<List<OtherEmployeesInfoViewModel>> GetInfoFromOtherEmployeesAsync(
            int idEmployeeWhoIsQuerying,
            string? search,
            string? filter,
            string? ordination
        );
        Task<bool> EmployeeEmailAlreadyVerifiedAsync(string email);

        Task<Employee?> GetByEmailAsync(string email);

        Task SaveChangesAsync();
    }
}
