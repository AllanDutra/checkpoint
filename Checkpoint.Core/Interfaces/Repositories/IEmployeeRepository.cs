using Checkpoint.Core.Entities;
using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Core.Interfaces.Repositories
{
#nullable enable
    public interface IEmployeeRepository
    {
        Task RegisterAsync(Employee employee);
        Task<bool> AlreadyAnEmployeeWithTheSameEmail(string email);
        Task<bool> AlreadyAnEmployeeWithTheSameUsername(string username);
        Task<EmployeeClaimsViewModel?> GetEmployeeClaimsByUsernameAndPassword(
            string username,
            string encryptedPassword
        );
        Task<List<OtherEmployeesInfoViewModel>> GetInfoFromOtherEmployees(
            int idEmployeeWhoIsQuerying,
            string? search,
            string? filter,
            string? ordination
        );
        Task<bool> EmployeeEmailAlreadyVerified(string email);
    }
}
