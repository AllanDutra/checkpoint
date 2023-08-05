using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpoint.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            return services;
        }
    }
}
