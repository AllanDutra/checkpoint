using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Core.Interfaces.Notifications
{
    public interface INotifier
    {
        void ClearNotification();
        bool HasNotification();
        List<NotificationModel> GetNotifications();
        void Handle(NotificationModel notification);
    }
}
