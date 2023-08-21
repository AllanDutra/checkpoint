using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint.Infrastructure.Persistence.Repositories
{
    public class PointLogRepository : IPointLogRepository
    {
        private readonly CheckpointDbContext _checkpointDbContext;

        public PointLogRepository(CheckpointDbContext checkpointDbContext)
        {
            _checkpointDbContext = checkpointDbContext;
        }

        public async Task RegisterAsync(PointLog pointLog)
        {
            await _checkpointDbContext.AddAsync(pointLog);

            await _checkpointDbContext.SaveChangesAsync();
        }

        public async Task<char?> GetLastCheckpoint(int employeeId)
        {
            var lastCheckpoint = await _checkpointDbContext.PointLogs
                .Where(l => l.EmpolyeeId == employeeId)
                .OrderByDescending(l => l.Date)
                .FirstOrDefaultAsync();

            if (lastCheckpoint != null)
                return lastCheckpoint.Type;

            return null;
        }
    }
}
