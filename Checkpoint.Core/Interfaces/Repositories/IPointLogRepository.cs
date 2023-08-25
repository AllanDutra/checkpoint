using Checkpoint.Core.Entities;

namespace Checkpoint.Core.Interfaces.Repositories
{
#nullable enable
    public interface IPointLogRepository
    {
        Task RegisterAsync(PointLog pointLog);
        Task<PointLog?> GetLastCheckpointAsync(int employeeId);
    }
}
