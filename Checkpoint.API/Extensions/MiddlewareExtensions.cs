using Checkpoint.API.Middlewares;

namespace Checkpoint.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<EmailVerificationMiddleware>();

            return services;
        }
    }
}
