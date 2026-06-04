namespace TravelBuddy.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime SentOn { get; set; }
        public bool IsRead { get; set; }
    }
}
