using Checkpoint.Core.Entities;

namespace Checkpoint.Core.Interfaces.Repositories
{
    public interface IPointLogRepository
    {
        Task RegisterAsync(PointLog pointLog);
        Task<char?> GetLastCheckpoint(int employeeId);
    }
}
