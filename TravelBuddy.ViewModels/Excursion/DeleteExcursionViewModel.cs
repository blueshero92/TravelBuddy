namespace TravelBuddy.ViewModels.Excursion
{
    public class DeleteExcursionViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public string Destination { get; set; } = null!;
    }
}
