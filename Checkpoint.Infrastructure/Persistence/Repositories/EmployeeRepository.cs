using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CheckpointDbContext _dbContext;

        public EmployeeRepository(CheckpointDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AlreadyAnEmployeeWithTheSameEmail(string email)
        {
            return await _dbContext.Employees.AnyAsync(employee => employee.Email == email);
        }

        public async Task<bool> AlreadyAnEmployeeWithTheSameUsername(string username)
        {
            return await _dbContext.Employees.AnyAsync(employee => employee.User == username);
        }

        public async Task<EmployeeClaimsViewModel?> GetEmployeeClaimsByUsernameAndPassword(
            string username,
            string encryptedPassword
        )
        {
            return await _dbContext.Employees
                .Where(e => e.User == username && e.Password == encryptedPassword)
                .Select(
                    e =>
                        new EmployeeClaimsViewModel(
                            e.Id,
                            e.Email,
                            e.Name,
                            e.User,
                            e.VerifiedEmail ?? false
                        )
                )
                .SingleOrDefaultAsync();
        }
    }
}
