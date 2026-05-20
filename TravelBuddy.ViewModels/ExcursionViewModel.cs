namespace TravelBuddy.ViewModels
{
    public class ExcursionViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
