using Checkpoint.Core.DomainServices.Crypto;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpoint.Core
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICryptoDomainService, CryptoDomainService>();

            return services;
        }
    }
}
