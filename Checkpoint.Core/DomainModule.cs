using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.DomainServices.Crypto;
using Checkpoint.Core.DomainServices.ExpiredEmailConfirmationChecker;
using Checkpoint.Core.DomainServices.Mail;
using Microsoft.Extensions.DependencyInjection;
using static Checkpoint.Core.DomainServices.ExpiredEmailConfirmationChecker.ExpiredEmailConfirmationCheckerDomainService;

namespace Checkpoint.Core
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICryptoDomainService, CryptoDomainService>();

            services.AddScoped<IAuthDomainService, AuthDomainService>();

            services.AddScoped<IMailDomainService, MailDomainService>();

            services.AddHostedService<ConsumeScopedServiceHostedDomainService>();

            services.AddScoped<
                IExpiredEmailConfirmationCheckerDomainService,
                ExpiredEmailConfirmationCheckerDomainService
            >();

            return services;
        }
    }
}
