using System.Reflection;
using Checkpoint.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint.Infrastructure.Persistence
{
    public class CheckpointDbContext : DbContext
    {
        public CheckpointDbContext(DbContextOptions<CheckpointDbContext> options)
            : base(options) { }

        public virtual DbSet<EmailVerification> EmailVerifications { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PointLog> PointLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
