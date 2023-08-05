using Checkpoint.Core.Entities;

namespace Checkpoint.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task RegisterAsync(Employee employee);
        Task<bool> AlreadyAnEmployeeWithTheSameEmail(string email);
        Task<bool> AlreadyAnEmployeeWithTheSameUsername(string username);
    }
}
