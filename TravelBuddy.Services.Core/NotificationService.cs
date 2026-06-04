using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Notification;

namespace TravelBuddy.Services.Core
{
    public class NotificationService : INotificationService
    {
        // Dependency injection of the database context to access notifications in the database.
        private readonly TravelBuddyDbContext dbContext;

        // Constructor to initialize the NotificationService with the provided TravelBuddyDbContext.
        public NotificationService(TravelBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Task for retrieving a list of notifications for a specific user, ordered by the date they were sent, and mapped to a view model for display.
        public async Task<IEnumerable<NotificationViewModel>> GetUserNotificationsAsync(Guid userId)
        {
            // Query the database for notifications belonging to the specified user.
            IEnumerable<NotificationViewModel> notificationsVm = await dbContext
                                                                    .Notifications
                                                                    .Where(n => n.UserId == userId)
                                                                    .OrderByDescending(n => n.SentOn)
                                                                    .Select(n => new NotificationViewModel
                                                                    {
                                                                        Id = n.Id,
                                                                        Message = n.Message,
                                                                        SentOn = n.SentOn,
                                                                        IsRead = n.IsRead
                                                                    })
                                                                    .ToListAsync();
            // Return the list of notifications as view models.
            return notificationsVm;

        }

        // Task for counting the number of unread notifications for a specific user.
        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            // Query the database to count the number of notifications for the specified user that are marked as unread (IsRead is false).
            return await dbContext
                        .Notifications
                        .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        // Task for marking all notifications as read for a specific user by updating the IsRead property of each unread notification and saving the changes to the database.
        public async Task MarkAllAsReadAsync(Guid userId)
        {
            // Query the database for all unread notifications belonging to the specified user.
            IEnumerable<Notification> unread = await dbContext
                                                    .Notifications
                                                    .Where(n => n.UserId == userId && !n.IsRead)
                                                    .ToListAsync();

            // Iterate through each unread notification and set its IsRead property to true.
            foreach (Notification notification in unread)
            {
                notification.IsRead = true;
            }

            // Save the changes to the database to persist the updated read status of the notifications.
            await dbContext.SaveChangesAsync();
        }
    }
}
