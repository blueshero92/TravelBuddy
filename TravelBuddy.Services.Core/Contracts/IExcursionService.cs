using TravelBuddy.ViewModels.Excursion;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IExcursionService
    {
        Task<IEnumerable<ExcursionViewModel>> GetAllExcursionsAsync();

        Task<IEnumerable<ExcursionViewModel>> SearchExcursionsAsync(string searchQuery);

        Task<ExcursionViewModel> GetExcursionByIdAsync(Guid id);

        Task<bool> AddExcursionAsync(ExcursionInputModel? excursionIm);

        Task<ExcursionInputModel> GetExcursionForEditByIdAsync(Guid excursionId);

        Task<bool> EditExcursionAsync(Guid excursionId, ExcursionInputModel? excursionIm);

        Task<DeleteExcursionViewModel?> GetExcursionForDeleteionAsync(Guid excursionId);

        Task<bool> DeleteExcursionAsync(Guid excursionId);

        Task<IEnumerable<ExcursionViewModel?>> GetUserFavoriteExcursionsAsync(Guid userId);

        Task<bool> AddExcursionToFavoritesAsync(Guid userId, Guid excursionId);

        Task<bool> RemoveExcursionFromFavoritesAsync(Guid userId, Guid excursionId);

    }
}
