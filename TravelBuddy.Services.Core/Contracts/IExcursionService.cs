using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IExcursionService
    {
        Task<IEnumerable<ExcursionViewModel>> GetAllExcursionsAsync();
    }
}
