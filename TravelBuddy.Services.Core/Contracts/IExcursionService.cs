using TravelBuddy.ViewModels.Excursion;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IExcursionService
    {
        Task<IEnumerable<ExcursionViewModel>> GetAllExcursionsAsync();

        Task<ExcursionViewModel> GetExcursionByIdAsync(Guid id);
    }
}
