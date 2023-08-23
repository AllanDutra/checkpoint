using Checkpoint.Core.Entities;
using Checkpoint.Core.Enums;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using Dapper;
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

        public async Task<List<OtherEmployeesInfoViewModel>> GetInfoFromOtherEmployees(
            int idEmployeeWhoIsQuerying,
            string? search,
            string? filter,
            string? ordination
        )
        {
            var query =
                @"WITH RankedPointLogs AS
                (SELECT PL.empolyee_id,
                        PL.date,
                        PL.type,
                        ROW_NUMBER() OVER (PARTITION BY PL.empolyee_id
                                            ORDER BY PL.date DESC) AS row_num
                FROM PointLogs PL)
                SELECT E.id,
                    E.name,
                    RPL.date AS LastPointLogDate,
                    RPL.type
                FROM Employees E
                LEFT JOIN RankedPointLogs RPL ON E.id = RPL.empolyee_id
                AND RPL.row_num = 1
                WHERE E.id != @idEmployeeWhoIsQuerying";

            if (search != null)
                query += $" AND (E.name LIKE @search OR E.id LIKE @search)";

            if (filter != null)
            {
                query +=
                    $" AND (RPL.type = {(filter.ToUpper() == EmployeesFilterEnum.AVAILABLE ? "'A'" : "'E' OR RPL.type IS NULL")})";
            }

            if (ordination != null)
            {
                query += $" ORDER BY E.name {ordination}";
            }
            else
            {
                query +=
                    @" ORDER BY CASE
                    WHEN RPL.type = 'A' THEN 1
                    WHEN RPL.type = 'E' THEN 2
                    WHEN RPL.type IS NULL THEN 3
                END,
                CASE
                    WHEN RPL.type = 'E' THEN RPL.date
                    ELSE NULL
                END DESC";
            }

            return (
                await _dbContext.Database
                    .GetDbConnection()
                    .QueryAsync<OtherEmployeesInfoViewModel>(
                        query,
                        new { idEmployeeWhoIsQuerying, search = $"%{search}%" }
                    )
            ).ToList();
        }
    }
}
