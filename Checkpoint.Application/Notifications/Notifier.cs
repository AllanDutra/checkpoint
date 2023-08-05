using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Application.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<NotificationModel> _notifications;

        public Notifier() => _notifications = new List<NotificationModel>();

        public void Handle(NotificationModel notification) => _notifications.Add(notification);

        public void ClearNotification()
        {
            if (_notifications.Any())
                _notifications.Clear();
        }

        public List<NotificationModel> GetNotifications() => _notifications;

        public bool HasNotification() => _notifications.Any();
    }
}
