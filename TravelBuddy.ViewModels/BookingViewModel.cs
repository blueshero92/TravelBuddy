namespace TravelBuddy.ViewModels
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid ExcursionId { get; set; }

        public string ExcursionTitle { get; set; } = null!;

        public string ExcursionDestination { get; set; } = null!;

        public DateTime ExcursionStartDate { get; set; }

        public DateTime ExcursionEndDate { get; set; }

        public decimal ExcursionPrice { get; set; }

        public string? ExcursionImageUrl { get; set; }

        public DateTime BookedOn { get; set; }

        public int Status { get; set; }
    }
}
