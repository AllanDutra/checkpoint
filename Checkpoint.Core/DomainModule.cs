using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.DomainServices.Crypto;
using Checkpoint.Core.DomainServices.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpoint.Core
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICryptoDomainService, CryptoDomainService>();

            services.AddScoped<IAuthDomainService, AuthDomainService>();

            services.AddScoped<IMailDomainService, MailDomainService>();

            return services;
        }
    }
}
