using Checkpoint.Application.Notifications;
using Checkpoint.Core.Interfaces.Notifications;

namespace Checkpoint.API.Extensions
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
