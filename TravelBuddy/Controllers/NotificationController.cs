using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Notification;

namespace TravelBuddy.Controllers
{
    public class NotificationController : BaseController
    {
        // Dependency Injection of the INotificationService to handle notification-related operations.
        private readonly INotificationService notificationService;

        // Constructor to initialize the NotificationController with the provided INotificationService.
        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        // Task for retrieving and displaying the current user's notifications on the Notifications page, while also marking all notifications as read.
        public async Task<IActionResult> Index()
        {
            // Retrieve the current user's ID (assuming you have a method to get it, e.g., from the authentication context).
            Guid userId = GetUserId();

            // Mark all notifications as read for the current user.
            await notificationService.MarkAllAsReadAsync(userId);

            // Retrieve the notifications for the current user.
            IEnumerable<NotificationViewModel> notifications = await notificationService.GetUserNotificationsAsync(userId);

            // Pass the notifications to the view for display.
            return View(notifications);
        }
    }
}
