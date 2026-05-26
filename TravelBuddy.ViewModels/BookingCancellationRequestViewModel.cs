namespace TravelBuddy.ViewModels
{
    public class BookingCancellationRequestViewModel
    {
        public Guid UserId { get; set; }
        public Guid BookingId { get; set; }
        public Guid ExcursionId { get; set; }
        public string ExcursionTitle { get; set; } = null!;
        public string ExcursionDestination { get; set; } = null!;
        public DateTime ExcursionStartDate { get; set; }
        public DateTime ExcursionEndDate { get; set; } = DateTime.Now;
        public decimal ExcursionPrice { get; set; }
        public string? ExcursionImageUrl { get; set; }
        public DateTime RequestedOn { get; set; }
        public string? Reason { get; set; }
        public int CancellationStatus { get; set; }
    }
}
