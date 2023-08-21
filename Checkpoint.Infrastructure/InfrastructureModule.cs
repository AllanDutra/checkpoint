using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Infrastructure.Persistence;
using Checkpoint.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpoint.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<CheckpointDbContext>(
                p => p.UseSqlServer(configuration.GetConnectionString("CheckpointDb"))
            );

            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IPointLogRepository, PointLogRepository>();

            return services;
        }
    }
}
