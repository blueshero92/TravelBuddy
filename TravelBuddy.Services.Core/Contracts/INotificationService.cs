using TravelBuddy.ViewModels.Notification;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationViewModel>> GetUserNotificationsAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task MarkAllAsReadAsync(Guid userId);
    }
}
