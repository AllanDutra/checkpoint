using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.DomainServices.Crypto;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpoint.Core
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICryptoDomainService, CryptoDomainService>();

            services.AddScoped<IAuthDomainService, AuthDomainService>();

            return services;
        }
    }
}
